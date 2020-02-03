using System;
using System.IO;

namespace ApiMobaileServise.Servise
{
    public static class Config
    {
        public static string AuchGoogleCloud {
            get
            {
                string path = File.ReadAllText("../AuchConfig/json.txt");
                return path;
            }
        }
    }
}
