using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DaoModels.DAO.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string path { get; set; }
        public string Base64
        {
            get
            {
                string tmpJson = JsonConvert.SerializeObject(File.ReadAllBytes(path));
                tmpJson = tmpJson.Replace("\"", "");
                return "";
            }
        }
    }
}
