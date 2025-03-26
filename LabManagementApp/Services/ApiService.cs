using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Laboratory>> GetLaboratoriesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Laboratory>>("api/laboratories");
    }

    public async Task<Laboratory> CreateLaboratoryAsync(Laboratory lab)
    {
        var response = await _httpClient.PostAsJsonAsync("api/laboratories", lab);
        return await response.Content.ReadFromJsonAsync<Laboratory>();
    }

    public async Task<List<Room>> GetRoomsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Room>>("api/rooms");
    }

    public async Task<List<Computer>> GetComputersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Computer>>("api/computers");
    }

    public async Task<bool> AssignComputerToRoomAsync(int computerId, int roomId)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/computers/{computerId}/assign-room/{roomId}", new { roomId });
        return response.IsSuccessStatusCode;
    }

}
