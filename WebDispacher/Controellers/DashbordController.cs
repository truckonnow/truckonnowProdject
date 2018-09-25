using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebDispacher.Service;

namespace WebDispacher.Controellers
{
    public class DashbordController : Controller
    {
        ManagerDispatch managerDispatch = new ManagerDispatch();
        
        [Route("Dashbord/Order/NewLoad")]
        public IActionResult NewLoad()
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                Request.Cookies.TryGetValue("KeyAvtho", out key);

                if (managerDispatch.CheckKey(key))
                {
                    actionResult = View("NewLoad");
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = Redirect("http://localhost:22929");
                }
            }
            catch (Exception)
            {

            }

            return actionResult;
        }
    }
}