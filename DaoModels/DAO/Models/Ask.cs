using System;
using System.Collections.Generic;
using System.Text;

namespace DaoModels.DAO.Models
{
    public class Ask
    {
        public int ID { get; set; }
        public string Lightbrightness { get; set; }
        public string Vehicle { get; set; }
        public string Enough_distance_to_take_pictures_at_least_4ft { get; set; }
        public string Weather_conditions  { get; set; }
        public string Does_The_vehicle_Starts  { get; set; }
        public string Does_The_vehicle_Drives { get; set; }
        public string Anyone_Rushing_you_to_perform_the_inspection { get; set; }
        public string How_far_is_the_vehicle_from_Trailer_Aprox_in_ft  { get; set; }
        public string Plate { get; set; }
        public string Exact_Mileage  { get; set; }
        public List<Photo> Any_personal_or_additional_items_with_or_in_vehicle{ get; set; }
        public List<Photo> Any_paperwork_or_documentation { get; set; }
        public string TypeVehicle { get; set; }
    }
}
