using System.Net.Http.Json;

public class LaboratoryService
{
    private readonly HttpClient _http;

    public LaboratoryService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Laboratory>> GetLaboratoriesAsync()
    {
        return await _http.GetFromJsonAsync<List<Laboratory>>("api/laboratories") ?? new List<Laboratory>();
    }

    public async Task<Laboratory?> GetLaboratoryByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<Laboratory>($"api/laboratories/{id}");
    }

    public async Task<bool> CreateLaboratoryAsync(Laboratory lab)
    {
        var response = await _http.PostAsJsonAsync("api/laboratories", lab);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateLaboratoryAsync(Laboratory lab)
    {
        var response = await _http.PutAsJsonAsync($"api/laboratories/{lab.Id}", lab);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteLaboratoryAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/laboratories/{id}");
        return response.IsSuccessStatusCode;
    }
}

