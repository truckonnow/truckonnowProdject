using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDispacher.Dao;

namespace WebDispacher.Service
{
    public class ManagerDispatch
    {
        SqlEntityFramworke _sqlEntityFramworke = null;

        public ManagerDispatch()
        {
            _sqlEntityFramworke = new SqlEntityFramworke();
        }
    }
}
