using System;
using System.IO;
using Xamarin.Forms;

namespace MDispatch.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string path { get; set; }
        public string Base64 { get; set; }
    }
}
