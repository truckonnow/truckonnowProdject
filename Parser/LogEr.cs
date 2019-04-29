using System;
using System.IO;

namespace Parser.Servise
{
    public class LogEr
    {

        private static void InfoLog(string infoText, string nameMethod, string date)
        {
            try
            {
                string curentDate = DateTime.Now.ToShortDateString().Replace('/', '.');
                File.AppendAllText($"./Log/AllLog/{curentDate}.txt", $"|INFO|Name method: {nameMethod}|Date: {date}|Info: {infoText}|{Environment.NewLine}");
                Console.Write($"|INFO|Name method: {nameMethod}|Date: {date}|Info: {infoText}|{Environment.NewLine}");
            }
            catch (Exception)
            {

            }
        }
        
        private static void ErrorLog(string infoText, string nameMethod, string date)
        {
            string curentDate = DateTime.Now.ToShortDateString().Replace('/', '.');
            File.AppendAllText($"./Log/AllLog/{curentDate}.txt", $"|Error|Name method: {nameMethod}|Date: {date}|Info: {infoText}|{Environment.NewLine}");
            File.AppendAllText($"./Log/Error/{curentDate}.txt", $"|Error|Name method: {nameMethod}|Date: {date}|Info: {infoText}|{Environment.NewLine}");
            Console.Write($"|Error|Name method: {nameMethod}|Date: {date}|Info: {infoText}|{Environment.NewLine}");
        }

        public static void Logerr(string typeData, string infoText, string nameMethod, string date)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            if (!Directory.Exists(currentDirectory+ "\\Log"))
            {
                Directory.CreateDirectory(currentDirectory+"\\Log\\AllLog");
                Directory.CreateDirectory(currentDirectory+"\\Log\\Error");
            }

            if (typeData == "Info")
            {
                InfoLog(infoText, nameMethod, date);
            }
            else if(typeData == "Error")
            {
                ErrorLog(infoText, nameMethod, date);
            }
        }
    }
}
