using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        int failedPings = 0;
        string jsonFilePath = "failed-pings.json";

        while (true)
        {
            Ping ping = new Ping();
             PingReply reply = ping.Send("google.com");

            if (reply.Status != IPStatus.Success)
            {
                failedPings++;
                var failedPingInfo = new { Number = failedPings, Status = reply.Status.ToString(), Timestamp = DateTime.Now };
                string json = JsonConvert.SerializeObject(failedPingInfo);
                File.AppendAllText(jsonFilePath, json + Environment.NewLine);
                Console.WriteLine($"Failed ping #{failedPings}: {reply.Status} at {DateTime.Now}");
            }
            else
            {
                Console.WriteLine("Ping successful");
            }

            Thread.Sleep(1000); // wait for 1 second before pinging again
        }
    }
}
