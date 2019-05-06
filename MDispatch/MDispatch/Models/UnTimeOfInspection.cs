using Prism.Mvvm;
using System;

namespace MDispatch.Models
{
    public class UnTimeOfInspection : BindableBase
    {
        public bool IsInspection { get; private set; }
        public string TimeOfInspection { get; private set; }

        public UnTimeOfInspection(string statusInspection)
        {
            bool IsInspectionDriver;
            bool IsInspectionToDayDriver;
            string[] arrData = statusInspection.Split(',');
            IsInspectionToDayDriver = Convert.ToBoolean(arrData[0]);
            IsInspectionDriver = Convert.ToBoolean(arrData[1]);
            TimeOfInspection = arrData[2] + " Hours";
            if (IsInspectionDriver)
            {
                if(IsInspectionToDayDriver)
                {
                    IsInspection = false;
                }
                else
                {
                    IsInspection = true;
                }
            }
            else
            {
                IsInspection = true;
            }
        }

        public string BoxColor
        {
            get
            {
                return "#fb2e2e";
            }
        }

        public string BoxColo1
        {
            get
            {
                string color = null;
                if(TimeOfInspection == "2 Hours" || IsInspection)
                {
                    color = "#fb2e2e";
                }
                else if(TimeOfInspection == "1 Hours")
                {
                    color = "#ff5a00";
                }
                return color;
            }
        }

        public string BoxColo2
        {
            get
            {
                string color = null;
                if (TimeOfInspection == "3 Hours" || IsInspection)
                {
                    color = "#fb2e2e";
                }
                else if (TimeOfInspection == "2 Hours")
                {
                    color = "#ff5a00";
                }
                else if(TimeOfInspection == "1 Hours")
                {
                    color = "#74DF00";
                }
                return color;
            }
        }

        public string BoxColor3
        {
            get
            {
                string color = null;
                if (TimeOfInspection == "4 Hours" || IsInspection)
                {
                    color = "#fb2e2e";
                }
                else if (TimeOfInspection == "3 Hours")
                {
                    color = "#ff5a00";
                }
                else if (TimeOfInspection == "1 Hours" || TimeOfInspection == "2 Hours")
                {
                    color = "#74DF00";
                }
                return color;
            }
        }

        public string BoxColor4
        {
            get
            {
                string color = null;
                if (TimeOfInspection == "5 Hours" || IsInspection)
                {
                    color = "#fb2e2e";
                }
                else if (TimeOfInspection == "4 Hours")
                {
                    color = "#ff5a00";
                }
                else if (TimeOfInspection == "1 Hours" || TimeOfInspection == "2 Hours" || TimeOfInspection == "3 Hours")
                {
                    color = "#74DF00";
                }
                return color;
            }
        }

        public string BoxColor5
        {
            get
            {
                string color = null;
                if (TimeOfInspection == "6 Hours" || IsInspection)
                {
                    color = "#fb2e2e";
                }
                else if (TimeOfInspection == "5 Hours")
                {
                    color = "#ff5a00";
                }
                else if (TimeOfInspection == "1 Hours" || TimeOfInspection == "2 Hours" || TimeOfInspection == "3 Hours" || TimeOfInspection == "4 Hours")
                {
                    color = "#74DF00";
                }
                return color;
            }
        }

        public string BoxColor6
        {
            get
            {
                string color = null;
                if (TimeOfInspection == "7 Hours" || IsInspection)
                {
                    color = "#fb2e2e";
                }
                else if (TimeOfInspection == "6 Hours")
                {
                    color = "#ff5a00";
                }
                else if (TimeOfInspection == "1 Hours" || TimeOfInspection == "2 Hours" || TimeOfInspection == "3 Hours" || TimeOfInspection == "4 Hours" || TimeOfInspection == "5 Hours")
                {
                    color = "#74DF00";
                }
                return color;
            }
        }

        public string TimeOfStatus
        {
            get
            {
                string status = null;
                if(IsInspection)
                {
                    status = "Pass inspection now";
                }
                else if(TimeOfInspection == "7 Hours" || TimeOfInspection == "6 Hours" || TimeOfInspection == "5 Hours" || TimeOfInspection == "4 Hours")
                {
                    status = "You can pass the inspection after";
                }
                else if(TimeOfInspection == "3 Hours" || TimeOfInspection == "2 Hours")
                {
                    status = "best time to pass inspection";
                }
                else if (TimeOfInspection == "1 Hours")
                {
                    status = "Pass inspection now";
                }
                return status;
            }
        }
    }
}
