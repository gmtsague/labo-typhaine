// See https://aka.ms/new-console-template for more information
using System;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Configuration;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

class Program
{
    static async Task Main()
    {
        
 // Build the configuration
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Ensure the app finds the JSON file
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        // Read values from configuration
        string serviceName = config["AppSettings:ServiceName"];
        string loggingLevel = config["AppSettings:LoggingLevel"];
        string connectionString = config["AppSettings:Database:ConnectionString"];
        string ApiEndpoint =  config["AppSettings:ApiComputerEndpoint"];

       // Display configuration values
        Console.WriteLine($"\n\nService Name: {serviceName}");
        Console.WriteLine($"Logging Level: {loggingLevel}");
        Console.WriteLine($"Database Connection String: {connectionString}");

        while (true)
        {
            //Console.WriteLine("\nCollecting machine info...");

            //await SaveMachineInfoWithoutApi(connectionString);
            
            await SaveMachineInfoUsingApi(ApiEndpoint);

            Console.WriteLine("Sleeping for 10 minutes...");
            Thread.Sleep(TimeSpan.FromMinutes(10));

        }
    }

    static async Task  SaveMachineInfoWithoutApi(string connectionString)
    {
        Console.WriteLine("\nCollecting machine info...");

        string ip = MachineInfoCollector.GetIpAddress();
        string mac = MachineInfoCollector.GetMacAddress();
        string ram = MachineInfoCollector.GetRamSize();
        string os = MachineInfoCollector.GetOperatingSystem();
        string machineName = MachineInfoCollector.GetMachineName();
        string diskSize = MachineInfoCollector.GetDiskSize();
        string diskBrand = MachineInfoCollector.GetDiskBrand();

        Console.WriteLine($"Name: {machineName}, IP: {ip}, MAC: {mac}, RAM: {ram}, OS: {os}, Disk Size: {diskSize}, Brand: {diskBrand}");

        // string connectionString = "server=your_server;database=MachineMonitoring;user=your_user;password=your_password;";
        var dbService = new DatabaseService(connectionString);

        await dbService.InsertMachineInfoAsync(machineName, ip, mac, ram, os, diskSize, diskBrand);
    }

    static async Task SaveMachineInfoUsingApi(string ApiEndpoint)
    {
        Console.WriteLine("\nCollecting machine info...");

        var computer = new Computer
        {
            IPAddress = MachineInfoCollector.GetIpAddress(),
            MacAddress = MachineInfoCollector.GetMacAddress(),
            RamSize = MachineInfoCollector.GetRamSize(),
            OperatingSystem = MachineInfoCollector.GetOperatingSystem(),
            DiskSize = MachineInfoCollector.GetDiskSize(),
            ComputerName = MachineInfoCollector.GetMachineName(),
            DiskBrand = MachineInfoCollector.GetDiskBrand()
        };

        Console.WriteLine($"Name: {computer.ComputerName}, IP: {computer.IPAddress}, MAC: {computer.MacAddress}, RAM: {computer.RamSize}, OS: {computer.OperatingSystem}, Disk Size: {computer.DiskSize}, Brand: {computer.DiskBrand}");

        await ApiClient.SaveMachineInfo(computer, ApiEndpoint);
    }
}

