using Microsoft.AspNetCore.Mvc;
using System;
using WebDispacher.Service;

namespace WebDispacher.Controellers
{
    public class ContactsController : Controller
    {
        ManagerDispatch managerDispatch = new ManagerDispatch();

        [Route("Contact/Contacts")]
        public IActionResult NewLoad(int page)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    ViewBag.Contacts = managerDispatch.GetContacts();
                    actionResult = View("FullContacts");
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = Redirect(Config.BaseReqvesteUrl);
                }
            }
            catch (Exception)
            {

            }
            return actionResult;
        }


        [HttpGet]
        [Route("Contact/CreateContact")]
        public IActionResult CreateContact()
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    actionResult = View("CreateContact");
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = Redirect(Config.BaseReqvesteUrl);
                }
            }
            catch (Exception)
            {

            }
            return actionResult;
        }

        [HttpPost]
        [Route("Contact/CreateContact")]
        public IActionResult CreateDriver(string fullName, string emailAddress, string phoneNumbe)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    if ((fullName != null && fullName != "") && (emailAddress != null && emailAddress != "") && (emailAddress != null && emailAddress != "")
                       && (fullName != null && fullName != ""))
                    {
                        managerDispatch.CreateContact(fullName, emailAddress, phoneNumbe);
                        actionResult = Redirect($"{Config.BaseReqvesteUrl}/Contact/Contacts");
                    }
                    else
                    {
                        actionResult = View("CreateContact");
                    }
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = Redirect(Config.BaseReqvesteUrl);
                }
            }
            catch (Exception)
            {

            }
            return actionResult;
        }
    }
}