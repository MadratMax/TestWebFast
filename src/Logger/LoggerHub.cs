using System;
using System.Collections.Generic;
using System.Text;

namespace TestWebFast
{
    class LoggerHub
    {
        public static List<string> PostponedErrors = new List<string>{""};

        public static List<string> PostponedInfos = new List<string>{""};

        public static string Notification;

        public static List<string> Notifications = new List<string> { "" };

        public static Dictionary<string, string> TestErrors = new Dictionary<string, string>();

        private static Dictionary<string, List<string>> Warnings = new Dictionary<string, List<string>>();

        public static string GetError(string testName)
        {
            try
            {
                return TestErrors[testName];
            }
            catch
            {
                return string.Empty;
            }
        }

        public static void RemoveError(string testName)
        {
            try
            {
                TestErrors[testName] = string.Empty;
            }
            catch
            {
                
            }
        }

        public static void AddWarningNotificationToHub(string testName, string notification)
        {
            try
            {
                Warnings.Add(testName, new List<string> { notification });
            }
            catch
            {
                Warnings[testName].Add(notification);
            }
        }

        public static void AddNotificationToHub(string notification)
        {
            Notifications.Add(notification);
        }

        public static Dictionary<string, List<string>> GetWarnings()
        {
            return Warnings;
        }

        public static List<string> GetNotifications()
        {
            return Notifications;
        }
    }
}
