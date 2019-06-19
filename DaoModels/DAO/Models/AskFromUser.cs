
namespace DaoModels.DAO.Models
{
    public class AskFromUser
    {
        public int id { get; set; }
        public string Your_Full_Name { get; set; }
        public string Your_phone { get; set; }
        public string How_many_keys_are_driver_been_given { get; set; }
        public string Any_titles_been_given_to_driver { get; set; }
        public string What_form_of_payment_are_you_using_to_pay_for_transportation { get; set; }
        public Photo App_will_ask_for_signature_of_the_client_signature { get; set; }
        public string CountPay { get; set; }
        public Photo PhotoPay { get; set; }
        public Video VideoRecord { get; set; }
        public string EmailPay { get; set; }
        public string NamePaymment { get; set; }
    }
}
