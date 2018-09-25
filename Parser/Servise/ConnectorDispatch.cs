using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Servise
{
    class ConnectorDispatch
    {
        private ParserDispatch parserDispatch = null;
        private List<string> pages = null;
        private HttpClient httpClient = null;

        public ConnectorDispatch()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new CookieContainer();
            handler.CookieContainer.Add(new Uri("https://www.centraldispatch.com"), new Cookie("name", "value"));
            httpClient = new HttpClient(handler);

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

        public async void Worker()
        {
            string sourse = null;
            byte[] sourseInByte = null;
            string uriReqvest = null;
            string baysUrl = "https://www.centraldispatch.com";
            string prefFirst = "/protected/cargo/dispatched-to-me?group=Dispatched%20To%20Me&folder=";
            string prefLast = "&sort=V&dir=1&page=";
            int countPage = 0;
            bool isPage = false;
            foreach (var page in pages)
            {
                do
                {
                    uriReqvest = $"{baysUrl}/{prefFirst}{countPage}{prefFirst}{page}";
                    sourse = await GetSourse(uriReqvest);
                    isPage = parserDispatch.CheckIsNextPage(sourse);
                    if (isPage)
                    {
                        countPage++;
                    }
                } while (isPage);
            }
            
        }

        private async Task<string> GetSourse(string uriReqvest)
        {
            string sourse = null;
            byte[] sourseInByte = null;
            var response = await httpClient.GetAsync("https://www.centraldispatch.com/protected/cargo/dispatched-to-me");
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                sourseInByte = await response.Content.ReadAsByteArrayAsync();
                sourse = Encoding.Default.GetString(sourseInByte, 0, sourseInByte.Length - 1);
            }
            else
            {
                //err Loger
            }
            return sourse;
        }
    }
}
