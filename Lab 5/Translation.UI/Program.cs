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
            String[] requests = new String[] { "/api/Translate/Get", "/api/Translate/GetInt/5", "/api/Translate/GetName/Chris", "/api/Translate/GetString/Hello" };

            foreach (String request in requests)
            {
                try
                {
                    Task<String> str = Program.GetStringAsync(request);

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

                catch
                {
                    Console.WriteLine("ERROR: couldn't resolve host");
                    Program.Client.Dispose();
                    return -1;
                }
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