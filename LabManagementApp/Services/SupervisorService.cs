using System.Net.Http.Json;

public class SupervisorService
{
    private readonly HttpClient _httpClient;

    public SupervisorService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Supervisor>> GetSupervisorsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Supervisor>>("api/supervisors");
    }

    public async Task<Supervisor> GetSupervisorByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Supervisor>($"api/supervisors/{id}");
    }

    public async Task CreateSupervisorAsync(Supervisor supervisor)
    {
        await _httpClient.PostAsJsonAsync("api/supervisors", supervisor);
    }

    public async Task UpdateSupervisorAsync(Supervisor supervisor)
    {
        await _httpClient.PutAsJsonAsync($"api/supervisors/{supervisor.Id}", supervisor);
    }

    public async Task DeleteSupervisorAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/supervisors/{id}");
    }
}
