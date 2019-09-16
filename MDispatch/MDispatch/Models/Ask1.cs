using System.Collections.Generic;

namespace MDispatch.Models
{
    public class Ask1
    {
        public int Id { get; set; }
        public string Exact_Mileage { get; set; }//
        public string Did_you_notice_any_mechanical_imperfections_wile_loading { get; set; }//
        public string Did_someone_help_you_load_it { get; set; }//
        public string Did_someone_load_the_vehicle_for_you { get; set; }//
        public string Did_you_Damage_anything_at_the_pick_up { get; set; }//
        public string What_method_of_exit_did_you_use { get; set; }//
        public string Did_you_jumped_the_vehicle_to_start { get; set; }//
        public string Have_you_used_winch { get; set; }//
        //public List<Photo> Any_additional_parts_been_given_to_you { get; set; }
        //public List<Photo> Any_additional_documentation_been_given_after_loading { get; set; }
        //public string How_many_keys_total_you_been_given { get; set; }
        //public string All_4_wheels_are_correctly_strapped_strapped { get; set; }
        //public string Type_of_strap { get; set; }
        public List<Photo> App_will_force_driver_to_take_pictures_of_each_strap { get; set; }//
        public List<Photo> Photo_after_loading_in_the_truck { get; set; }//
    }
}
