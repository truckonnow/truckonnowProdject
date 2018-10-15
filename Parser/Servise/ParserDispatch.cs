using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using DaoModels.DAO.Models;
using Newtonsoft.Json;
using Parser.DAO;
using System;
using System.Collections.Generic;
using System.IO;
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
        SqlCommandParser sqlCommandParser = new SqlCommandParser();

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


        public void ParseDataInUrl(string sourse, string UrlReqvest)
        {
            Shipping shipping = new Shipping();
            try
            {
                LogEr.Logerr("Info", $"Parsing of html data", "ParseDataInUrl", DateTime.Now.ToShortTimeString());
                shipping.UrlReqvest = UrlReqvest;
                SetHeadInform(sourse, ref shipping);
                SetOrderInform(sourse, ref shipping);
                SetContactInform(sourse, ref shipping);
                SetPickupInform(sourse, ref shipping);
                SetDeliveryInform(sourse, ref shipping);
                SetDispatchInform(sourse, ref shipping);
                SetVehicleInform(sourse, ref shipping);
                LogEr.Logerr("Info", $"Successful parsing of their html, Load id {shipping.Id}", "ParseDataInUrl", DateTime.Now.ToShortTimeString());
            }
            catch(Exception)
            {
                LogEr.Logerr("Info", $"Unsuccessful parsing of their html, Load id {shipping.Id}", "ParseDataInUrl", DateTime.Now.ToShortTimeString());
            }
            Task.Run(async() => await sqlCommandParser.AddOrder(shipping));
        }

        private void SetHeadInform(string sourse, ref Shipping shipping)
        {
            IHtmlDocument htmlDocument = htmlParser.Parse(sourse);
            var element = htmlDocument.GetElementsByClassName("col-xs-12 col-sm-7 col-md-8")[0]
                .GetElementsByTagName("p");
            shipping.Id = element[0].TextContent.Remove(0, element[0].TextContent.IndexOf(": ") + 2);
            shipping.CurrentStatus = "NewLoad"; //element[1].TextContent.Remove(0, element[1].TextContent.IndexOf(": ") + 2);
            shipping.LastUpdated = element[2].TextContent.Remove(0, element[2].TextContent.IndexOf(": ") + 2);
            shipping.CDReference = element[3].TextContent.Remove(0, element[3].TextContent.IndexOf(": ") + 2);
        }

        private void SetOrderInform(string sourse, ref Shipping shipping)
        {
            IHtmlDocument htmlDocument = htmlParser.Parse(sourse);
            var element = htmlDocument.GetElementById("sheetDetails")
                .GetElementsByClassName("panel panel-default")[1]
                .GetElementsByClassName("panel-body")[0]
                .GetElementsByClassName("col-xs-12 col-sm-6");
            var el = element[0].GetElementsByTagName("p");
            shipping.DispatchDate = el[0].TextContent.Remove(0, el[0].TextContent.IndexOf("Dispatch Date: ")+"Dispatch Date: ".Length);
            shipping.DispatchDate = shipping.DispatchDate.Remove(shipping.DispatchDate.IndexOf("\n"));
            shipping.PickupExactly = el[0].TextContent.Remove(0, el[0].TextContent.IndexOf("Pickup Exactly: ") + "Pickup Exactly: ".Length);
            shipping.PickupExactly = shipping.PickupExactly.Remove(shipping.PickupExactly.IndexOf("\n"));
            shipping.DeliveryEstimated = el[0].TextContent.Remove(0, el[0].TextContent.IndexOf("Delivery Estimated: ") + "Delivery Estimated: ".Length);
            shipping.DeliveryEstimated = shipping.DeliveryEstimated.Remove(shipping.DeliveryEstimated.IndexOf("\n"));
            shipping.ShipVia = el[1].TextContent.Remove(0, el[1].TextContent.IndexOf(": ") + 2);
            shipping.Condition = el[2].TextContent.Remove(0, el[2].TextContent.IndexOf(": ") + 2);
            shipping.PriceListed = element[1].TextContent.Remove(0, element[1].TextContent.IndexOf("Price Listed:")+ "Price Listed:".Length);
            shipping.PriceListed = shipping.PriceListed.Remove(shipping.PriceListed.IndexOf("\n"));
            shipping.TotalPaymentToCarrier = element[1].TextContent.Remove(0, element[1].TextContent.IndexOf("Total Payment to Carrier: ")+ "Total Payment to Carrier: ".Length);
            shipping.TotalPaymentToCarrier = shipping.TotalPaymentToCarrier.Remove(shipping.TotalPaymentToCarrier.IndexOf(" \n"));
            shipping.OnDeliveryToCarrier = element[1].TextContent.Remove(0, element[1].TextContent.IndexOf("to Carrier:\n")+ "to Carrier:\n".Length);
            shipping.OnDeliveryToCarrier = shipping.OnDeliveryToCarrier.Remove(0, shipping.OnDeliveryToCarrier.IndexOf("\n")+2).TrimStart();
            shipping.OnDeliveryToCarrier = shipping.OnDeliveryToCarrier.Remove(shipping.OnDeliveryToCarrier.IndexOf("\n"));
            shipping.CompanyOwesCarrier = element[1].TextContent.Remove(0, element[1].TextContent.IndexOf("Company")+ "Company** owes Carrier:\n".Length);
            shipping.CompanyOwesCarrier = shipping.CompanyOwesCarrier.Remove(0, shipping.CompanyOwesCarrier.IndexOf("\n")).TrimStart();
            shipping.CompanyOwesCarrier = shipping.CompanyOwesCarrier.Remove(shipping.CompanyOwesCarrier.IndexOf("\n"));
        }

        private void SetContactInform(string sourse, ref Shipping shipping)
        {
            try
            {
                IHtmlDocument htmlDocument = htmlParser.Parse(sourse);
                var element = htmlDocument.GetElementById("sheetDetails")
                    .GetElementsByClassName("col-xs-12 col-sm-6 col-md-4")[1]
                    .GetElementsByClassName("panel panel-default")[0]
                    .GetElementsByClassName("panel-body")[0];
                shipping.ContactC = element.TextContent.Remove(0, element.TextContent.IndexOf("Contact: ") + "Contact: ".Length);
                shipping.ContactC = shipping.ContactC.Remove(shipping.ContactC.IndexOf("\n")).Trim();
                shipping.PhoneC = element.TextContent.Remove(0, element.TextContent.IndexOf("Phone: ") + "Phone: ".Length);
                shipping.PhoneC = shipping.PhoneC.Remove(shipping.PhoneC.IndexOf("F")).Trim();
                shipping.FaxC = element.TextContent.Remove(0, element.TextContent.IndexOf("Fax: ") + "Fax: ".Length);
                shipping.FaxC = shipping.FaxC.Remove(shipping.FaxC.IndexOf("I")).Trim();
                shipping.IccmcC = element.TextContent.Remove(0, element.TextContent.IndexOf("ICCMC: ") + "ICCMC: ".Length);
                shipping.IccmcC = shipping.IccmcC.Trim();
            }
            catch (Exception)
            {

            }
        }

        private void SetPickupInform(string sourse, ref Shipping shipping)
        {   
            IHtmlDocument htmlDocument = htmlParser.Parse(sourse);
            var element = htmlDocument.GetElementById("sheetBottom")
                .GetElementsByClassName("col-xs-12 col-sm-6 col-md-4")[0]
                .GetElementsByClassName("panel panel-default")[0]
                .GetElementsByClassName("panel-body")[0].Children;
            shipping.ContactNameP = element[0].TextContent.Trim();
            shipping.ContactNameP = shipping.ContactNameP.Remove(shipping.ContactNameP.IndexOf("\n"));
            shipping.CoordinatesP = element[0].TextContent.Trim();
            shipping.CoordinatesP = shipping.CoordinatesP.Remove(0, shipping.CoordinatesP.IndexOf("\n") + 2).Trim(); ;
            shipping.PhoneP = element[2].TextContent;
        }

        private void SetDeliveryInform(string sourse, ref Shipping shipping)
        {
            try
            {
                IHtmlDocument htmlDocument = htmlParser.Parse(sourse);
                var element = htmlDocument.GetElementById("sheetBottom")
                    .GetElementsByClassName("col-xs-12 col-sm-6 col-md-4")[1]
                    .GetElementsByClassName("panel panel-default")[0]
                    .GetElementsByClassName("panel-body")[0].Children;
                shipping.ContactNameD = element[0].TextContent.Trim();
                shipping.ContactNameD = shipping.ContactNameD.Remove(shipping.ContactNameD.IndexOf("\n"));
                shipping.CoordinatesD = element[0].TextContent.Trim();
                string tempStr = shipping.CoordinatesD.Remove(0, shipping.CoordinatesD.IndexOf("\n") + 2).Trim();
                if (tempStr[0] == '(')
                {
                    string subName = $" {tempStr.Remove(tempStr.IndexOf(')') + 1)}";
                    shipping.CoordinatesD += $" {subName}";
                    tempStr = tempStr.Remove(0, subName.Length).Trim();
                }
                shipping.CoordinatesD = tempStr;
                shipping.PhoneD = element[2].TextContent;
            }
            catch (Exception)
            {

            }
        }

        private void SetVehicleInform(string sourse, ref Shipping shipping)
        {
            shipping.VehiclwInformations = new List<VehiclwInformation>();
            VehiclwInformation vehiclwInformation = null;
            IHtmlDocument htmlDocument = htmlParser.Parse(sourse);
            var vehicles = htmlDocument.GetElementById("sheetBottom")
                .GetElementsByClassName("panel panel-default")[0]
                .GetElementsByClassName("panel-body table-responsive")[0]
                .GetElementsByClassName("table table-striped table-hover")[0]
                .GetElementsByTagName("tbody")[0].GetElementsByTagName("tr");
            foreach(var vehicle in vehicles)
            {
                vehiclwInformation = new VehiclwInformation();
                vehiclwInformation.Year = vehicle.Children[1].TextContent.Trim();
                vehiclwInformation.Make = vehicle.Children[2].TextContent.Trim();
                vehiclwInformation.Model = vehicle.Children[3].TextContent.Trim();
                vehiclwInformation.Type = vehicle.Children[4].TextContent.Trim();
                vehiclwInformation.Color = vehicle.Children[5].TextContent.Trim();
                vehiclwInformation.Plate = vehicle.Children[6].TextContent.Trim();
                vehiclwInformation.VIN = vehicle.Children[7].TextContent.Trim();
                vehiclwInformation.Lot = vehicle.Children[8].TextContent.Trim();
                vehiclwInformation.AdditionalInfo = vehicle.Children[9].TextContent.Trim();
                shipping.VehiclwInformations.Add(vehiclwInformation);
            }
        }

        private void SetDispatchInform(string sourse, ref Shipping shipping)
        {
            IHtmlDocument htmlDocument = htmlParser.Parse(sourse);
            var element = htmlDocument.GetElementById("sheetBottom")
                .GetElementsByClassName("col-xs-12 col-md-4")[2]
                .Children[0].Children[1];
            shipping.Titl1DI = element.TextContent;
        }
    }
}
