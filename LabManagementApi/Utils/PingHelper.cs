using System.Net.NetworkInformation;

public static class PingHelper
{
    public static bool IsComputerOnline(string ipAddress)
    {
        try
        {
            using Ping ping = new();
            PingReply reply = ping.Send(ipAddress, 1000); // Timeout = 1000ms (1 second)
            return reply.Status == IPStatus.Success;
        }
        catch
        {
            return false;
        }
    }
}
