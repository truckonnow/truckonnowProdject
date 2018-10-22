using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using xNet;

namespace Parser.Servise
{
    class ConnectorDispatch
    {
        private ParserDispatch parserDispatch = null;
        private List<string> pages = null;
        public string Tokene { get; set; }
        private HttpRequest httpRequest = null;
        private CookieDictionary cooks;

        public ConnectorDispatch()
        {
            pages = new List<string>()
            {
                "Dispatched",
                "Picked-Up",
                "Delivered",
                "Cancelled",
                "Archived"
            };
            parserDispatch = new ParserDispatch();
        }

        private void Avthorization()
        {
            try
            {
                LogEr.Logerr("Info", "Login start", "Avthorization", DateTime.Now.ToShortTimeString());
                Init();
                var htm = httpRequest.Get("https://www.centraldispatch.com/login?uri=%2Fprotected%2F").ToString();
                Tokene = Regex.Match(htm, @"CSRFToken.{4}lue\W\W(\w+)").Groups[1].Value;
                httpRequest.AddParam("Username", "Gts2012");
                httpRequest.AddParam("Password", "dispatch35211");
                httpRequest.AddParam("r", "");
                httpRequest.AddParam("CSRFToken", Tokene);
                var res = httpRequest.Post("https://www.centraldispatch.com/login?uri=/protected/");
                cooks = httpRequest.Cookies;
                if (res.Cookies.Count > 6)
                {
                    LogEr.Logerr("Info", "Login successful", "Avthorization", DateTime.Now.ToShortTimeString());
                }
                else
                {
                    LogEr.Logerr("Error", "Not successful authorization 'wrong password or login'", "Avthorization", DateTime.Now.ToShortTimeString());
                }
            }
            catch (Exception)
            {
                LogEr.Logerr("Error", "Critical authorization error'", "Avthorization", DateTime.Now.ToShortTimeString());
            }
        }

        private void Init()
        {
            httpRequest = new HttpRequest()
            {
                Cookies = new CookieDictionary(),
                AllowAutoRedirect = true
            };
        }

        public void Worker()
        {
            Avthorization();
            string url = "https://www.centraldispatch.com";
            string pref1 = "/protected/cargo/dispatched-to-me?group=Dispatched%20To%20Me&folder=";
            string pref2 = "&sort=V&dir=1&page=";
            Task.Run(async () =>
            {
                Init();
                httpRequest.Cookies = cooks;
                bool isNextParsePage = false;
                int countLnck2 = 0;
                foreach (var page in pages)
                {
                    int countLnck1 = 0;
                    LogEr.Logerr("Info", $"Start pulling links on the status order: {page}", "Worker", DateTime.Now.ToShortTimeString());
                    int coutnPage = 0;
                    do
                    {
                        List<string> urlsPages = null;
                        string fullPageHtml = httpRequest.Get($"{url}{pref1}{page}{pref2}{coutnPage}").ToString();
                        await Task.Run(() => urlsPages = parserDispatch.ParseInManyUrl(fullPageHtml));
                        if (urlsPages.Count != 0)
                        {
                            LogEr.Logerr("Info", $"Count Page : {urlsPages.Count}", "Worker", DateTime.Now.ToShortTimeString());
                            int countLick = 0;
                            foreach (var urlpage in urlsPages)
                            {
                                try
                                {
                                    string sourseUrl = httpRequest.Get(urlpage).ToString();
                                    await Task.Run(() => parserDispatch.ParseDataInUrl(sourseUrl, urlpage));
                                    countLick++;
                                }
                                catch (Exception)
                                {
                                    LogEr.Logerr("Error", $"Unsuccessful html pulling by reference: {urlpage}", "Worker", DateTime.Now.ToShortTimeString());
                                    //https://www.centraldispatch.com//protected/dispatch/view?dsid=17990722
                                }
                            }
                            LogEr.Logerr("Info", $"The number of successfully elongated html for links: {countLick}", "Worker", DateTime.Now.ToShortTimeString());
                            isNextParsePage = parserDispatch.CheckIsNextPage(fullPageHtml);
                            coutnPage++;
                        }
                        else
                        {
                            LogEr.Logerr("Info", $"The number of successfully elongated html for links: {0}", "Worker", DateTime.Now.ToShortTimeString());
                            isNextParsePage = false;
                        }
                    } while (isNextParsePage);
                    coutnPage = 0;
                    LogEr.Logerr("Info", $"The number of successfully elongated html for links: {countLnck1} on the page with statuses {page}", "Worker", DateTime.Now.ToShortTimeString());

                }
                LogEr.Logerr("Info", $"The number of total elongated html links: {countLnck2}", "Worker", DateTime.Now.ToShortTimeString());
            });
        }
    }
}
