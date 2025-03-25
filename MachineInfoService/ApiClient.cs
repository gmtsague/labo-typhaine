using System.Net.Http.Json;
using System.Threading.Tasks;

public class ApiClient
{
    private static readonly HttpClient client = new();

    public static async Task SaveMachineInfo(Computer computer, string ApiComputerEndpoint)
    {
        try
        {
            // await client.PostAsJsonAsync("http://localhost:5029/api/computers", computer);
          var response =  await client.PostAsJsonAsync(ApiComputerEndpoint, computer);

            if (response.IsSuccessStatusCode)
                Console.WriteLine("Machine info saved to database.");
            else
            {
                var ErrMsg = await response.Content.ReadAsStringAsync();
                Console.WriteLine("\nEchec de l'enregistrement des données vers le serveur API");
                Console.WriteLine(@"==> Details: {0}", ErrMsg?.ToString());
            }
               
        } catch (Exception err)
        {
            Console.WriteLine("\nEchec de l'enregistrement des données vers le serveur API");
            Console.WriteLine(@"==> Details: {0}", err.Message);
        }

    }
}
