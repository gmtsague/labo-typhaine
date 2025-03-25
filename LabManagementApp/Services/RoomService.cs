using LabManagementApp.Pages;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

public class RoomService
{
    private readonly HttpClient _httpClient;

    public RoomService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Room>> GetRoomsAsync()
    {
          return await _httpClient.GetFromJsonAsync<List<Room>>("api/rooms");

        //List<Room> rooms = new();
        //try
        //{
        //    // HttpResponseMessage response = await Http.GetAsync("https://your-api-url/api/rooms");

        //    HttpResponseMessage response = await _httpClient.GetFromJsonAsync<List<Room>>("api/rooms");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        rooms = (await response.Content.ReadFromJsonAsync<List<Room>>()) ?? [];
        //    }
        //    else
        //    {
        //        var errorMessage = await response.Content.ReadAsStringAsync();
        //        Console.Error.WriteLine($"Error fetching rooms: {errorMessage}");
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Console.Error.WriteLine($"Exception: {ex.Message}");
        //}
        //return rooms;
    }

    public async Task<Room> GetRoomByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Room>($"api/rooms/{id}");
    }

    public async Task CreateRoomAsync(Room room)
    {
        await _httpClient.PostAsJsonAsync("api/rooms", room);
    }

    public async Task UpdateRoomAsync(Room room)
    {
        await _httpClient.PutAsJsonAsync($"api/rooms/{room.Id}", room);
    }

    public async Task DeleteRoomAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/rooms/{id}");
    }
}
