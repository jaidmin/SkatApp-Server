using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Client
{
    class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            Console.WriteLine("Hello World!");
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine( tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);


            // api call

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            var response2 = await client.GetAsync("http://localhost:5001/api/values/5");
            if (!response2.IsSuccessStatusCode)
            {
                Console.WriteLine(response2.StatusCode);
            }
            else
            {
                var content2 = await response2.Content.ReadAsStringAsync();
                Console.WriteLine(content2);
            }


        }
    }
}