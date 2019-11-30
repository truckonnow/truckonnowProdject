namespace ApiMobaileServise.Servise.GoogleApi
{
    public static class SelectDetect
    {
        public static IDetect GetDetectType(string type)
        {
            IDetect detect = null;
            if (type == "Truc")
            {
                detect = new DerectTruck();
            }
            else if(type == "Trailer")
            {
                detect = new DerectTruck();
            }
            return detect;
        }
    }
}