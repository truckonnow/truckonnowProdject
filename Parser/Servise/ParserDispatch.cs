using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using DaoModels.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Servise
{
    public class ParserDispatch
    {
        HtmlParser htmlParser = new HtmlParser();
        
        public bool CheckIsNextPage(string sourse)
        {
            bool isPageNext = false;
            IHtmlDocument htmlDocument = htmlParser.Parse(sourse);
            var elements = htmlDocument.GetElementsByClassName("col-xs-6 text-center");
            if (elements != null)
            {
                string elementCountPageStr = elements[0].InnerHtml;
                string countOrderStr = elementCountPageStr.Remove(0, elementCountPageStr.IndexOf("-")+1);
                countOrderStr = countOrderStr.Remove(countOrderStr.IndexOf(" "));
                string fullCountOrderStr = elementCountPageStr.Remove(0, elementCountPageStr.IndexOf("of ") +3);
                fullCountOrderStr = fullCountOrderStr.Remove(1);
                if(fullCountOrderStr != countOrderStr)
                {
                    isPageNext = true;
                }
            }
            return isPageNext;
        }

        public List<string> ParseInManyUrl(string sourse)
        {
            List<string> urlsPages = new List<string>();
            IHtmlDocument htmlDocument = htmlParser.Parse(sourse);
            var element = htmlDocument.GetElementById("dispatch-table");
            if(element != null)
            {
                var elems = element.GetElementsByTagName("tbody")[0]
                    .GetElementsByTagName("tr");
                foreach (var elem in elems)
                {
                    var el = elem.GetElementsByClassName("hidden-xs")
                        .Last().GetElementsByTagName("a")[1];
                    string urlPage = el.OuterHtml.Remove(0, el.OuterHtml.IndexOf("'")+1);
                    urlPage = urlPage.Remove(urlPage.IndexOf("'"));
                    urlsPages.Add($"https://www.centraldispatch.com/{urlPage}");
                }
            }
            return urlsPages;
        }
        

        public void ParseDataInUrl(string sourse)
        {
            Shipping shipping = new Shipping();
            IHtmlDocument htmlDocument = htmlParser.Parse(sourse);
            var element = htmlDocument.GetElementsByClassName("col-xs-12 col-sm-7 col-md-8")[0]
                .GetElementsByTagName("p");
            shipping.Id = element[0].TextContent.Remove(0, element[0].TextContent.IndexOf(": ")+2);
            shipping.CurrentStatus = element[1].TextContent.Remove(0, element[1].TextContent.IndexOf(": ") + 2);
        }
    }
}
