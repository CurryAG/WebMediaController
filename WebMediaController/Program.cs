using Microsoft.AspNetCore.Hosting;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace WebMediaController
{
    public class Program
    {
        public static string ServerAdress;
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ServerAdress = $"http://{GetIPAdress()[0]}:5000";
            }
            else
            {
                ServerAdress = $"http://{args[0]}:5000";
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<MainPage>();
                    webBuilder.UseUrls(ServerAdress);
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        Process.Start("cmd", $"/c start {ServerAdress}");
                    }
                });
        public static List<string> GetIPAdress()
        {
            List<string> list = new List<string>();
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

            // Ищем интерфейс WiFi
            NetworkInterface wifiInterface = interfaces.FirstOrDefault(iface =>
                iface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
                iface.OperationalStatus == OperationalStatus.Up &&
                iface.Supports(NetworkInterfaceComponent.IPv4));

            // Если интерфейс WiFi найден, то получаем его IP-адреса
            if (wifiInterface != null)
            {
                IPInterfaceProperties properties = wifiInterface.GetIPProperties();
                foreach (UnicastIPAddressInformation unicast in properties.UnicastAddresses)
                {
                    if (unicast.Address.AddressFamily != AddressFamily.InterNetwork)
                    {
                        continue;
                    }
                    if (IPAddress.IsLoopback(unicast.Address) == true)
                    {
                        continue;
                    }
                    Ping ping = new Ping();
                    PingReply reply = ping.Send(unicast.Address.ToString(), 1000);
                    if (reply.Status == IPStatus.Success)
                    {
                        list.Add(unicast.Address.ToString());
                    }
                }
            }
            else // Если интерфейс WiFi не найден, то ищем первый Ethernet интерфейс
            {
                NetworkInterface ethernetInterface = interfaces.FirstOrDefault(iface =>
                    iface.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    iface.OperationalStatus == OperationalStatus.Up &&
                    iface.Supports(NetworkInterfaceComponent.IPv4));

                // Если Ethernet интерфейс найден, то получаем его IP-адреса
                if (ethernetInterface != null)
                {
                    IPInterfaceProperties properties = ethernetInterface.GetIPProperties();
                    foreach (UnicastIPAddressInformation unicast in properties.UnicastAddresses)
                    {
                        if (unicast.Address.AddressFamily != AddressFamily.InterNetwork)
                        {
                            continue;
                        }
                        if (IPAddress.IsLoopback(unicast.Address) == true)
                        {
                            continue;
                        }
                        Ping ping = new Ping();
                        PingReply reply = ping.Send(unicast.Address.ToString(), 1000);
                        if (reply.Status == IPStatus.Success)
                        {
                            list.Add(unicast.Address.ToString());
                        }
                    }
                }
            }
            return list;
        }
    }
}