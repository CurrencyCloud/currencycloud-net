using System;
using System.Threading.Tasks;
using CurrencyCloud;
using CurrencyCloud.Exception;

namespace Cookbook
{
    class Program
    {
        static void Main()
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            Client client = new Client();

            try
            {
                string token = await client.LoginAsync(CurrencyCloud.Environment.ApiServer.Demo, "test.it@mailinator.com1", "b5266326b1855443544626f188b8a234da99e1c36d91819419e17091b4f0a7f4");
            }
            catch(ApiException ex)
            {
                Console.WriteLine(ex.ToYamlString());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey(false);
        }
    }
}
