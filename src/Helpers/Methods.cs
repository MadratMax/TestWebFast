namespace TestWebFast
{
    using System;
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using System.Threading;

    public static class Methods
    {
        public static bool WaitUntil(Func<bool> action, int maxWait = 15)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            bool result;

            while (!(result = action()) && watch.ElapsedMilliseconds < (maxWait * 1000))
            {
                Thread.Sleep(1000);
            }

            watch.Stop();
            return result;
        }

        public static void ActionWhileCondition(Action action, Func<bool> condition, int maxLeadSeconds = 100, int stepIntervalInMilliseconds = 1000)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (condition())
            {
                action();
                if (sw.Elapsed > TimeSpan.FromSeconds(maxLeadSeconds))
                {
                    break;
                }

                Thread.Sleep(stepIntervalInMilliseconds);
            }

            sw.Stop();
        }

        public static void WaitForSeconds(int maxWait = 60)
        {
            Thread.Sleep(maxWait * 1000);
        }

        public static string RemoveRestrictedChars(string input)
        {
            char[] symbols = { ':', '*', '|', '/', '\\', '?', '"', '<', '>', '\'', '\r', '\n' };
            foreach (char symbol in symbols)
            {
                input = input.Replace(symbol, ' ');
            }

            int index = input.IndexOf(",", StringComparison.Ordinal);

            if (index > 0)
            {
                input = input.Substring(0, index);
            }

            return input;
        }

        public static string TruncateAndNormalize(this string value, int maxChars)
        {
            var updatedString = value.Length <= maxChars ? value : value.Substring(0, maxChars).Trim();

            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            updatedString = regex.Replace(updatedString, " ");

            return updatedString;
        }

        public static void KillChromeDriver()
        {
            foreach (var process in Process.GetProcessesByName("chromedriver"))
            {
                try
                {
                    process.Kill();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
