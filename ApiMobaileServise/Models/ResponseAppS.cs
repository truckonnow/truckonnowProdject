using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMobaileServise.Models
{
    public class ResponseAppS
    {
        public string Status { get; set; }
        public string Description { get; set; }
        public object ResponseStr { get; set; }

        public ResponseAppS(string status, string description, object responseStr)
        {
            Status = status;
            Description = description;
            ResponseStr = responseStr;
        }
    }
}