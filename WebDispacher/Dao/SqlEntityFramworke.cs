using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDispacher.Dao
{
    public class SqlEntityFramworke
    {
        private Context context = null;
       
        public SqlEntityFramworke()
        {
            context = new Context();
        }
    }
}
