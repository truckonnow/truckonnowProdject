using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebDispacher.Service;

namespace WebDispacher.Controellers
{
    public class RAController : Controller
    {
        ManagerDispatch managerDispatch = null;

        public IActionResult Index()
        {
            return View("Avthorization");
        }

        [HttpPost]
        public IActionResult Avthorization(string Email, string Password)
        {
            managerDispatch = new ManagerDispatch();
            try
            {
                if (Email == null || Password == null)
                    throw new Exception();
                
            }
            catch(Exception)
            {

            }
            return null;
        }
    }
}