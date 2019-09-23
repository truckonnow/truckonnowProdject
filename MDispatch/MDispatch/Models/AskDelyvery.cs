
using System.Collections.Generic;

namespace MDispatch.Models
{
    public class AskDelyvery
    {
        public int ID { get; set; }
        public string Time_Of_Delivery { get; set; }
        public string Safe_delivery_location_Truck_and_trailer_parked_on { get; set; }//
        public string Did_you_meet_the_client { get; set; }//
        public string Truck_on_emergency_brake { get; set; }//
        public string Truck_locked { get; set; }//
        public string All_locks_on_the_trailer { get; set; }//
        public string Vehicle_parked_in_the_safe_location { get; set; }//
        public List<Photo> Please_take_a_picture_Id_of_the_person_taking_the_delivery { get; set; }//
        public string Lightbrightness { get; set; }
        public string Vehicle_Condition_on_delivery  { get; set; }//
        public string Weather_Conditions { get; set; }//
        public string How_did_you_get_inside_of_the_vehicle { get; set; }//
        public string Did_the_vehicle_starts { get; set; }//
        public string Does_the_vehicle_Drives  { get; set; }//
        public string Anyone_Rushing_you_to_perform_the_delivery { get; set; }//
        public string How_Far_is_the_Trailer_from_Delivery_destination { get; set; }
        public string Exact_mileage_after_unloading { get; set; }//
        public string Anyone_helping_you_unload { get; set; }//
        public string Did_someone_else_unloaded_the_vehicle_for_you { get; set; }//
        public string Did_you_notice_any_imperfections_on_body_wile_vehicle_been_transported { get; set; }//
        public string After_inspecting_the_car_press_the_confirm_button { get; set; }//
    }
}
