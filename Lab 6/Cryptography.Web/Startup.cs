using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Cryptography.Web
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration) => this.Configuration = configuration;

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddCookie("Bearer")
				.AddOAuth("Microsoft", options =>
				{
					options.ClientId = this.Configuration["Authentication:Microsoft:ClientId"];
					options.ClientSecret = this.Configuration["Authentication:Microsoft:ClientSecret"];
					options.CallbackPath = new PathString("/signin");
					options.AuthorizationEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize";
					options.TokenEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/token";
					options.UserInformationEndpoint = "https://graph.microsoft.com/v1.0/me";
					options.Scope.Add("user.read");
					options.ClaimActions.MapJsonKey(ClaimTypes.Name, "displayName");

					options.Events = new OAuthEvents
					{
						OnCreatingTicket = async context =>
						{
							HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
							request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
							request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
							HttpResponseMessage response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
							JsonDocument user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
							context.RunClaimActions(user.RootElement);
						}
					};
				});

			services.AddControllersWithViews();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthorization();
			app.UseAuthentication();

			app.UseEndpoints(endpoints => endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}"));
		}
	}
}
