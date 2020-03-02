using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Translation.UI
{
    internal static class Program
    {
        private const String URI = "http://localhost:5000";

        private static HttpClient Client = new HttpClient();

        private async static Task<String> GetStringAsync(String path)
        {
            HttpResponseMessage response = await Program.Client.GetAsync(path);
            return await response.Content.ReadAsStringAsync();
        }

        private async static Task<int> Main(String[] args)
        {
            Program.Client.BaseAddress = new Uri(Program.URI);

            try
            {
                Task<String> str = Program.GetStringAsync("/api/Translate/GetString/Hello");

                if (await Task.WhenAny(str, Task.Delay(20000)) == str)
                {
                    Console.WriteLine(str.Result);
                }

                else
                {
                    Console.WriteLine("ERROR: request timed out");
                    Program.Client.Dispose();
                    return -1;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex);
            }
            
            Program.Client.Dispose();

            if (!Console.IsInputRedirected)
            {
                Console.Write("Press any key to exit...");
                Console.ReadKey(true);
                Console.WriteLine();
            }

            return 0;
        }
    }
}