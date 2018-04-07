using System;
using System.Net.Http;
using IdentityModel.Client;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //AuthenByCredentials();
            //Console.WriteLine();
            AuthenByUsernamePassword();
            Console.WriteLine();

            Console.Read();
        }

        static async void AuthenByCredentials()
        {
            Console.WriteLine(nameof(AuthenByCredentials));

            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:1000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "TestClientCredentials", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("testapi");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:2000/api/values/10");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                
            }
            else Console.WriteLine(response.StatusCode);
        }

        static async void AuthenByUsernamePassword()
        {
            Console.WriteLine(nameof(AuthenByUsernamePassword));

            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:1000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "TestResourceOwnerPassword", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("duy","duy","testapi");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:2000/api/values/20");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);

            }
            else Console.WriteLine(response.StatusCode);
        }
    }
}
