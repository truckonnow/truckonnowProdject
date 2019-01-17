using System;
using System.Collections.Generic;
using System.Text;

namespace DaoModels.DAO.Models
{
    public class AskDelyvery
    {
        public int ID { get; set; }
        public string Time_Of_Delivery { get; set; }
        public string Lightbrightness { get; set; }
        public string Vehicle_Condition_on_delivery { get; set; }
        public string Weather_Conditions { get; set; }
        public string How_did_you_get_inside_of_the_vehicle { get; set; }
        public string Did_the_vehicle_starts { get; set; }
        public string Does_the_vehicle_Drives { get; set; }
        public string Anyone_Rushing_you_to_perform_the_delivery { get; set; }
        public string How_Far_is_the_Trailer_from_Delivery_destination { get; set; }
        public string Exact_mileage_after_unloading { get; set; }
        public string Anyone_helping_you_unload { get; set; }
        public string Did_someone_else_unloaded_the_vehicle_for_you { get; set; }
        public string Did_you_notice_any_imperfections_on_body_wile_vehicle_been_transported { get; set; }
        public string How_many_keys_are_you_giving_to_client { get; set; }
        public string Are_you_giving_any_paperwork_to_a_client { get; set; }
        public string Did_client_inspected_the_vehicle { get; set; }
    }
}
