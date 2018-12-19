using System;
using System.Collections.Generic;
using System.Text;

namespace MDispatch.Models
{
    public class PhotoInspection
    {
        public int Id { get; set; }
        public int IndexPhoto { get; set; }
        public List<Photo> Photos { get; set; }
    }
}
