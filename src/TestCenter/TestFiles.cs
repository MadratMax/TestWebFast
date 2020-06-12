using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestWebFast
{
    public class TestFiles
    {
        private static List<FileInfo> Tests;
        private static List<string> TestNames;

        public static List<FileInfo> GetTestCollection()
        {
            GetFiles();
            return Tests;
        }

        public static List<string> GetTestNames()
        {
            return TestNames;
        }

        private static void GetFiles()
        {
            Tests = new List<FileInfo>();
            TestNames = new List<string>();
            DirectoryInfo d = new DirectoryInfo(Settings.TestsPath);
            if (!d.Exists)
            {
                Logger.WritePostponedError("Tests folder was not found");
                return;
            }
            FileInfo[] Files = d.GetFiles($"*{Settings.TestsExtension}");
            string str = "";
            foreach (FileInfo file in Files)
            {
                Tests.Add(file);

                var testName = Path.GetFileNameWithoutExtension(file.FullName);

                TestNames.Add(testName);
            }
        }
    }
}
