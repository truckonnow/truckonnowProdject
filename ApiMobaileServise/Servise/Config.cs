using System;
using System.IO;

namespace ApiMobaileServise.Servise
{
    public static class Config
    {
        public static string Url { get; set; } = "http://192.168.31.44:8099";
        public static string UrlAdmin { get; set; } = "http://192.168.31.44";
        public static string AuchGoogleCloud {
            get
            {
                string path = File.ReadAllText("../AuchConfig/json.txt");
                return path;
            }
        }
    }
}
