using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;

namespace DaoModels.DAO.Models
{
    public class PhotoDriver
    {
        public int Id { get; set; }
        public int IdInspaction { get; set; }
        public int IndexPhoto { get; set; }
        public string path { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Base64
        {
            set
            {
                try
                {
                    byte[] photoInArrayByte = Convert.FromBase64String(value);
                    if (!Directory.Exists(path))
                    {
                        string pathTmp = path.Remove(path.LastIndexOf("/"));
                        Directory.CreateDirectory(pathTmp);
                    }
                    using (var imageFile = new FileStream(path, FileMode.Create))
                    {
                        imageFile.Write(photoInArrayByte, 0, photoInArrayByte.Length);
                        imageFile.Flush();
                    }
                }
                catch (Exception)
                {

                }
            }
            get
            {
                if (path != null)
                {
                    try
                    {
                        string tmpJson = JsonConvert.SerializeObject(File.ReadAllBytes(path));
                        tmpJson = tmpJson.Replace("\"", "");
                        return tmpJson;
                    }
                    catch (Exception e)
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
        }

        [NotMapped]
        public string Base64_1 { get; set; }
    }
}
