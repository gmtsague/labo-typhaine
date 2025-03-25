using System;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;

public class MachineInfoCollector
{
    public static string GetIpAddress()
    {
        return Dns.GetHostAddresses(Dns.GetHostName())
                  .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?
                  .ToString() ?? "Unknown";
    }

    public static string GetMacAddress()
    {
        return NetworkInterface.GetAllNetworkInterfaces()
                .Where(nic => nic.NetworkInterfaceType != NetworkInterfaceType.Loopback && nic.OperationalStatus == OperationalStatus.Up)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault() ?? "Unknown";
    }

    public static string GetRamSize()
    {
        using var searcher = new ManagementObjectSearcher("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem");
        foreach (var obj in searcher.Get())
        {
            return (Convert.ToDouble(obj["TotalPhysicalMemory"]) / (1024 * 1024 * 1024)).ToString("F2") + " GB";
        }
        return "Unknown";
    }

    public static string GetOperatingSystem()
    {
        return Environment.OSVersion.ToString();
    }

    public static string GetMachineName()
    {
        return Environment.MachineName.ToString();
    }

    public static string GetDiskSize()
    {
        using var searcher = new ManagementObjectSearcher("SELECT Size FROM Win32_DiskDrive");
        foreach (var obj in searcher.Get())
        {
            return (Convert.ToDouble(obj["Size"]) / (1024 * 1024 * 1024)).ToString("F2") + " GB";
        }
        return "Unknown";
    }

    public static string GetDiskBrand()
    {
        using var searcher = new ManagementObjectSearcher("SELECT Model FROM Win32_DiskDrive");
        foreach (var obj in searcher.Get())
        {
            return obj["Model"].ToString();
        }
        return "Unknown";
    }
}
