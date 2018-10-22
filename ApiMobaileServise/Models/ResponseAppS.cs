using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMobaileServise.Models
{
    public class ResponseAppS<T>
    {
        public string Status { get; set; }
        public string Description { get; set; }
        public string StatusCode { get; set; }
        public T ResponseStr { get; set; }
    }
}
