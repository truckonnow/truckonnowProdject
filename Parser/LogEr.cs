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
                File.AppendAllText($"./Log/Service/AllLog/{curentDate}.txt", $"|INFO|Name method: {nameMethod}|Date: {date}|Info: {infoText}|{Environment.NewLine}");
                Console.Write($"|INFO|Name method: {nameMethod}|Date: {date}|Info: {infoText}|{Environment.NewLine}");
            }
            catch (Exception e)
            {

            }
        }

        private static void InfoLog1(string infoText, string nameMethod, string date)
        {
            try
            {
                string curentDate = DateTime.Now.ToShortDateString().Replace('/', '.');
                File.AppendAllText($"./Log/Service1/AllLog/{curentDate}.txt", $"|INFO|Name method: {nameMethod}|Date: {date}|Info: {infoText}|{Environment.NewLine}");
                Console.Write($"|INFO|Name method: {nameMethod}|Date: {date}|Info: {infoText}|{Environment.NewLine}");
            }
            catch (Exception)
            {

            }
        }

        private static void ErrorLog(string infoText, string nameMethod, string date)
        {
            string curentDate = DateTime.Now.ToShortDateString().Replace('/', '.');
            File.AppendAllText($"./Log/Service/AllLog/{curentDate}.txt", $"|Error|Name method: {nameMethod}|Date: {date}|Info: {infoText}|{Environment.NewLine}");
            File.AppendAllText($"./Log/Service/Error/{curentDate}.txt", $"|Error|Name method: {nameMethod}|Date: {date}|Info: {infoText}|{Environment.NewLine}");
            Console.Write($"|Error|Name method: {nameMethod}|Date: {date}|Info: {infoText}|{Environment.NewLine}");
        }

        public static void Logerr(string typeData, string infoText, string nameMethod, string date)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            if (!Directory.Exists(currentDirectory+ "\\Log"))
            {
                Directory.CreateDirectory(currentDirectory+ "\\Log\\Service\\AllLog");
                Directory.CreateDirectory(currentDirectory+ "\\Log\\Service\\Error");
                Directory.CreateDirectory(currentDirectory + "\\Log\\Service1\\AllLog");
                Directory.CreateDirectory(currentDirectory + "\\Log\\Service1\\Error");
            }

            if (typeData == "Info")
            {
                InfoLog(infoText, nameMethod, date);
            }
            else if (typeData == "Info1")
            {
                InfoLog1(infoText, nameMethod, date);
            }
            else if(typeData == "Error")
            {
                ErrorLog(infoText, nameMethod, date);
            }
        }
    }
}