using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TestWebFast
{
    class Logger
    {
        public static void WriteInfo(string info)
        {
            Console.WriteLine($"[INFO] {info}");
        }

        public static void WriteError(string error)
        {
            Console.WriteLine($"[ERROR] {error}");
        }

        public static void WritePostponedInfo(string info)
        {
            info = $" [INFO] {info}";
            LoggerHub.PostponedInfos.Add(info);
        }

        public static void WritePostponedError(string error)
        {
            //if (!LoggerHub.PostponedErrors.Contains(error))
            {
                error = $" [ERROR] {error}";
                LoggerHub.PostponedErrors.Add(error);
            }
        }

        public static void AddNotification(string note)
        {
            note = $" [INFO] {note}";
            LoggerHub.Notification = note;
            
        }

        public static void WriteRealTimeInfo(string info)
        {
            Console.WriteLine($" ___________________________________________________________________________________________________");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($" [INFO] {info}");
            Console.ResetColor();
            Console.WriteLine($" ___________________________________________________________________________________________________");
            Console.WriteLine($" press any key");
            Console.WriteLine($" ___________________________________________________________________________________________________");
            Thread.Sleep(2500);
        }

        public static void WriteRealTimeError(string error)
        {
            Console.Clear();
            Console.WriteLine($" ___________________________________________________________________________________________________");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($" [ERROR] {error}");
            Console.ResetColor();
            Console.WriteLine($" ___________________________________________________________________________________________________");
            Console.WriteLine($" press any key");
            Console.WriteLine($" ___________________________________________________________________________________________________");
            Thread.Sleep(2500);
        }

        public static void ClearHub()
        {
            LoggerHub.TestErrors.Clear();
            LoggerHub.Notification = string.Empty;
            LoggerHub.Notifications.Clear();
            LoggerHub.PostponedErrors = new List<string> {""};
            LoggerHub.PostponedInfos = new List<string> { "" };
        }
    }
}
