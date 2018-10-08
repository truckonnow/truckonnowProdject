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
            Avthorization();
        }

        private void Avthorization()
        {
            Init();
            var htm = httpRequest.Get("https://www.centraldispatch.com/login?uri=%2Fprotected%2F").ToString();
            Tokene = Regex.Match(htm, @"CSRFToken.{4}lue\W\W(\w+)").Groups[1].Value;
            httpRequest.AddParam("Username", "Gts2012");
            httpRequest.AddParam("Password", "dispatch35211");
            httpRequest.AddParam("r", "");
            httpRequest.AddParam("CSRFToken", Tokene);
            var res = httpRequest.Post("https://www.centraldispatch.com/login?uri=/protected/");
            cooks = httpRequest.Cookies;
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
            
            string url = "https://www.centraldispatch.com";
            string pref1 = "/protected/cargo/dispatched-to-me?group=Dispatched%20To%20Me&folder=";
            string pref2 = "&sort=V&dir=1&page=";
            try
            {
                if (cooks == null)
                    throw new Exception();
                //int i = 0;
                Task.Run(async() =>
                {
                    Init();
                    httpRequest.Cookies = cooks;
                    bool isNextParsePage = false;
                    foreach (var page in pages)
                    {
                        int coutnPage = 0;
                        do
                        {
                            List<string> urlsPages = null;
                            string fullPageHtml = httpRequest.Get($"{url}{pref1}{page}{pref2}{coutnPage}").ToString();
                            await Task.Run(() => urlsPages = parserDispatch.ParseInManyUrl(fullPageHtml));
                            int i = 0;
                            foreach(var urlpage in urlsPages)
                            {
                                string sourseUrl = httpRequest.Get(urlpage).ToString();
                                Task.Run(() => parserDispatch.ParseDataInUrl(sourseUrl));
                            }
                            isNextParsePage = parserDispatch.CheckIsNextPage(fullPageHtml);
                            coutnPage++;
                        } while (isNextParsePage);
                        coutnPage = 0;
                    }
                });
            }
            catch(Exception)
            {

            }
        }

        
    }
}
