using System;
using System.IO;

namespace ApiMobaileServise.Servise
{
    public static class Config
    {
        public static string Url { get; set; } = "http://212.224.113.5:8099"; //212.224.113.5
        public static string UrlAdmin { get; set; } = "http://truckonnow.com"; //http://truckonnow.com 192.168.31.44
        public static string AuchGoogleCloud {
            get
            {
                string path = File.ReadAllText("../AuchConfig/json.txt");
                return path;
            }
        }
    }
}
