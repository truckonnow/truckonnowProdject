using Prism.Mvvm;
using System;

namespace MDispatch.Models
{
    public class UnTimeOfInspection : BindableBase
    {
        public bool IsInspection { get; private set; } = false;
        public bool ISMaybiInspection { get; set; }
        public string TimeOfInspection { get; private set; }
        public string IdDriver { get; private set; }

        public UnTimeOfInspection(string statusInspection = null)
        {
            bool IsInspectionDriver;
            bool IsInspectionToDayDriver;
            if (statusInspection != null)
            {
                string[] arrData = statusInspection.Split(',');
                IsInspectionToDayDriver = Convert.ToBoolean(arrData[0]);
                IsInspectionDriver = Convert.ToBoolean(arrData[1]);
                TimeOfInspection = arrData[2] + " Hours";
                IdDriver = arrData[3];
                if (IsInspectionDriver)
                {
                    ISMaybiInspection = true;
                    if (IsInspectionToDayDriver)
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
                    ISMaybiInspection = false;
                    IsInspection = true;
                }
            }
        }

        public string BoxColor
        {
            get
            {
                return "#fb2e2e";
            }
        }

        public string BoxColor1
        {
            get
            {
                string color = "#FFFFFF";
                if(TimeOfInspection == "1 Hours" || TimeOfInspection == "2 Hours" || TimeOfInspection == "3 Hours" || TimeOfInspection == "4 Hours" || TimeOfInspection == "5 Hours")
                {
                    color = "#fb2e2e";
                }
                else if(TimeOfInspection == "6 Hours")
                {
                    color = "#ff5a00";
                }
                else if (TimeOfInspection == "7 Hours")
                {
                    color = "#74DF00";
                }
                else if (TimeOfInspection == "0 Hours")
                {
                    color = "#fb2e2e";
                }
                return color;
            }
        }

        public string BoxColor2
        {
            get
            {
                string color = "#FFFFFF";
                if (TimeOfInspection == "1 Hours" || TimeOfInspection == "2 Hours" || TimeOfInspection == "3 Hours" || TimeOfInspection == "4 Hours")
                {
                    color = "#fb2e2e";
                }
                else if (TimeOfInspection == "5 Hours")
                {
                    color = "#ff5a00";
                }
                else if(TimeOfInspection == "7 Hours" || TimeOfInspection == "6 Hours")
                {
                    color = "#74DF00";
                }
                else if (TimeOfInspection == "0 Hours")
                {
                    color = "#fb2e2e";
                }
                return color;
            }
        }

        public string BoxColor3
        {
            get
            {
                string color = "#FFFFFF";
                if (TimeOfInspection == "1 Hours" || TimeOfInspection == "2 Hours" || TimeOfInspection == "3 Hours")
                {
                    color = "#fb2e2e";
                }
                else if (TimeOfInspection == "4 Hours")
                {
                    color = "#ff5a00";
                }
                else if (TimeOfInspection == "7 Hours" || TimeOfInspection == "6 Hours" || TimeOfInspection == "5 Hours")
                {
                    color = "#74DF00";
                }
                else if(TimeOfInspection == "0 Hours")
                {
                    color = "#fb2e2e";
                }
                return color;
            }
        }

        public string BoxColor4
        {
            get
            {
                string color = "#FFFFFF";
                if (TimeOfInspection == "1 Hours" || TimeOfInspection == "2 Hours")
                {
                    color = "#fb2e2e";
                }
                else if (TimeOfInspection == "3 Hours")
                {
                    color = "#ff5a00";
                }
                else if (TimeOfInspection == "7 Hours" || TimeOfInspection == "6 Hours" || TimeOfInspection == "5 Hours" || TimeOfInspection == "4 Hours")
                {
                    color = "#74DF00";
                }
                else if (TimeOfInspection == "0 Hours")
                {
                    color = "#fb2e2e";
                }
                return color;
            }
        }

        public string BoxColor5
        {
            get
            {
                string color = "#FFFFFF";
                if (TimeOfInspection == "1 Hours")
                {
                    color = "#fb2e2e";
                }
                else if (TimeOfInspection == "2 Hours")
                {
                    color = "#ff5a00";
                }
                else if (TimeOfInspection == "7 Hours" || TimeOfInspection == "6 Hours" || TimeOfInspection == "5 Hours" || TimeOfInspection == "4 Hours" || TimeOfInspection == "2 Hours")
                {
                    color = "#74DF00";
                }
                else if (TimeOfInspection == "0 Hours")
                {
                    color = "#fb2e2e";
                }
                return color;
            }
        }

        public string BoxColor6
        {
            get
            {
                string color = "#FFFFFF";
                if (TimeOfInspection == "0 Hours" )
                {
                    color = "#fb2e2e";
                }
                else if (TimeOfInspection == "1 Hours")
                {
                    color = "#ff5a00";
                }
                else if (TimeOfInspection == "2 Hours" || TimeOfInspection == "3 Hours" || TimeOfInspection == "4 Hours" || TimeOfInspection == "5 Hours" || TimeOfInspection == "6 Hours" || TimeOfInspection == "7 Hours")
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
                string status = "";
                if(TimeOfInspection == "7 Hours" || TimeOfInspection == "6 Hours" || TimeOfInspection == "5 Hours" || TimeOfInspection == "4 Hours")
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
                else if (TimeOfInspection == "0 Hours")
                {
                    status = "Pass inspection now";
                }
                return status;
            }
        }
    }
}