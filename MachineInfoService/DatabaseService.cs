using System;
using MySqlConnector;
using System.Threading.Tasks;

public class DatabaseService
{
    private readonly string _connectionString;

    public DatabaseService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task InsertMachineInfoAsync(string ComputerName, string ip, string mac, string ram, string os, string diskSize, string diskBrand)
    {
        const string query = @"
            INSERT INTO Computers (ComputerName, IPAddress, MacAddress, RamSize, OperatingSystem, DiskSize, DiskBrand, SaveDate) 
            VALUES (@ComputerName, @IPAddress, @MacAddress, @RAM, @OS, @DiskSize, @DiskBrand, NOW())";

        try
        {
            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            if (connection.State == System.Data.ConnectionState.Open)
            {
                await using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ComputerName", ComputerName);
                command.Parameters.AddWithValue("@IPAddress", ip);
                command.Parameters.AddWithValue("@MacAddress", mac);
                command.Parameters.AddWithValue("@RAM", ram);
                command.Parameters.AddWithValue("@OS", os);
                command.Parameters.AddWithValue("@DiskSize", diskSize);
                command.Parameters.AddWithValue("@DiskBrand", diskBrand);

                await command.ExecuteNonQueryAsync();
                Console.WriteLine("Machine info saved to database.");
            }
            else
            {
                Console.WriteLine("\nImpossible de se connecter à la base de données MySql.\n");
                return;
            }

        }
        catch (Exception err)
        {
            Console.WriteLine("\nImposible de se connecter à la base de données MySql.");
            Console.WriteLine(@"==> L'enregistrement des données collectées a été annulé.");
            Console.WriteLine(@"==> Details: {0}", err.Message);
            Console.WriteLine(@"======================================================");
        }
    }
}
