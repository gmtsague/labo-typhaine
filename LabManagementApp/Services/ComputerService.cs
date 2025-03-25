using System.Net.Http.Json;

public class ComputerService
{
    private readonly HttpClient _httpClient;

    public ComputerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Computer>> GetComputersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Computer>>("api/computers");
    }

    public async Task<Computer> GetComputerByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Computer>($"api/computers/{id}");
    }

    public async Task CreateComputerAsync(Computer computer)
    {
        await _httpClient.PostAsJsonAsync("api/computers", computer);
    }

    public async Task UpdateComputerAsync(Computer computer)
    {
        await _httpClient.PutAsJsonAsync($"api/computers/{computer.Id}", computer);
    }

    public async Task DeleteComputerAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/computers/{id}");
    }
}
