using DaoModels.DAO.Models;

namespace ApiMobaileServise.EmailSmtp
{
    public class PaternSourse
    {
        public string GetPaternBol(Shipping shipping)
        {
            string patern = "<div style='width:700px;'>"
        + "<p><h3>BILL OF LADING / VEHICLE INSPECTION REPORT</h3></p>"
        + "<div style='border-style: solid; border-width: 2px;'>"
            + $"<span>LOAD ID: {shipping.idOrder}</span>"
            + "<div style='display: flex; justify-content: space-between;'>"
                + "<div style'border-style: solid; border-width: 2px; width: 50%;'>"
                    + "<span style='margin:60px;font-size:20px'>ORIGIN</span>"
                    + "<div style='margin:5px;' class='addresBlock'>"
                        + "<span>"
                            + $"{shipping.AddresP} {shipping.CityP} {shipping.CityP} <br />"
                            + $"{shipping.StateP} {shipping.ZipP} <br />"
                            + $"Contact: {shipping.NameP}<br />"
                            + $"Phone: {shipping.PhoneP}"
                        + "</span>"
                    + "</div>"
                + "</div>"
                + "<div style'border-style: solid; border-width: 2px; width: 50%;'>"
                    + "<span style='margin:60px;font-size:20px' > DESTINATION </span>"
                    + "<div style='margin:5px;' class='addresBlock'>"
                        + "<span>"
                            + $"{shipping.AddresD} {shipping.CityD} {shipping.CityD} <br />"
                            + $"{shipping.StateD} {shipping.ZipD} <br />"
                            + $"Contact: {shipping.NameD}<br />"
                            + $"Phone: {shipping.PhoneD}"
                        + "</span>"
                    + "</div>"
                + "</div>"
            + "</div>";
            foreach (var vech in shipping.VehiclwInformations)
            {
                patern += "<div style='display: flex; justify-content: space-between;'>"
                    + "<div style='border-style: solid; border-width: 2px; width:33%;'>"
                        + "<span style='margin:40px;font-size:18px'> VIN</span>"
                        + "<div style='margin:5px;' class='addresBlock'>"
                            + "<span style='font-weight:600;'>"
                            + vech.VIN
                            + "</span>"
                        + "</div>"
                    + "</div>"
                    + "<div style='border-style: solid; border-width: 2px; width:33%;'>"
                        + "<span style='margin:40px;font-size:18px'> Year / Make / Model </span>"
                        + "<div style='margin:5px;' class='addresBlock'>"
                            + "<span style='font-weight:600;'>"
                                + vech.Year + " " + vech.VIN + " " + vech.Model
                            + "</span>"
                        + "</div>"
                    + "</div>"
                    + "<div style='border-style: solid; border-width: 2px; width:33%;'>"
                        + "<span style='margin:40px;font-size:18px'>Type</span>"
                        + "<div style='margin:5px;' class='addresBlock'>"
                            + "<span style='font-weight: 600;'>"
                                + vech.Type
                            + "</span>"
                        + "</div>"
                    + "</div>"
                + "</div>";
            }
            patern += "<div style='border-style:solid; border-width 2px; width:100%;'>";
            foreach (var vech in shipping.VehiclwInformations)
            {
                patern += $"<img src='data:image/png;base64,{vech.Scan.Base64}'/> <br/>"
                       + $"<span style='margin:10px;'> See inspection photos: http://truckonnow.com/Photo/BOL/{vech.Id}</span>";
            }
            patern += "</div>";
            if(shipping.VehiclwInformations[0].AskFromUser != null && shipping.VehiclwInformations[0].AskFromUser.App_will_ask_for_signature_of_the_client_signature != null)
            {
                patern += "<div style='display: flex; justify-content: space-between;'>"
                    + "<div style'border-style: solid; border-width: 2px; width: 50%;'>"
                        + "<span style='margin:5px; font-weight: 600;' >"
                            + "I agree with the Driver's assessment of the"
                            + "condition of this vehicle."
                        + "</span>" 
                        + "<br />"
                        + "<span style='margin:5px;'>"
                            + "Origin Signature"
                        + "</span>"
                        + "<br />"
                        + $"<img style='margin:5px;' src='data:image/png;base64,{shipping.VehiclwInformations[0].AskFromUser.App_will_ask_for_signature_of_the_client_signature.Base64}' />"
                    + "</div>";
            }
            if (shipping.VehiclwInformations[0].askForUserDelyveryM != null && shipping.VehiclwInformations[0].askForUserDelyveryM.App_will_ask_for_signature_of_the_client_signature != null)
            {
                patern += "<div style'border-style: solid; border-width: 2px; width: 50%;'>"
                        + "<span style='margin:5px; font-weight:600;' >"
                            + "Vehicle received in good condition except as"
                            + "noted above."
                        + "</span>"
                        + "<br />"
                        + "<span style='margin:5px;' >"
                            + "Destination Signature"
                        + "</span>"
                        + "<br />"
                        + $"<img style='margin:5px;' src='data:image/png;base64,{shipping.VehiclwInformations[0].askForUserDelyveryM.App_will_ask_for_signature_of_the_client_signature.Base64}' />"
                    + "</div>"
                + "</div>"
            + "</div>"
       + "</div>";
            }
            return patern += "</div>";
        }

        public string GetPaternCopon()
        {
            string patern = "<p>Thank you for using our company, here is your 10% discount coupon.</p>";
            return patern;
        }
    }
}