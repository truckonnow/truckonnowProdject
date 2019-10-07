using System;
using System.Collections.Generic;
using System.Text;

namespace MDispatch.Models
{
    public class Ask
    {
        public int ID { get; set; }
        public string Lightbrightness { get; set; }
        public string Vehicle { get; set; }
        public string Weather_conditions { get; set; }
        public string Anyone_Rushing_you_to_perform_the_inspection { get; set; }
        public string Plate { get; set; }
        public List<Photo> Any_personal_or_additional_items_with_or_in_vehicle { get; set; }
        public string TypeVehicle { get; set; }
        public string Safe_delivery_location { get; set; }
        public string How_far_from_trailer { get; set; }
        public string Name_of_the_person_who_gave_you_keys { get; set; }
        public string Enough_space_to_take_pictures { get; set; }
    }
}
