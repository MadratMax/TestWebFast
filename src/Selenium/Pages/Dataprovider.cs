using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TestWebFast.CommandCenter;

namespace TestWebFast.Selenium.Pages
{
    class DataProvider
    {
        private static List<FileInfo> pages;
        private static List<string> pageNames;

        public DataProvider()
        {
            GetFiles();
        }

        public List<FileInfo> GetPageFiles()
        {
            return pages;
        }

        public static List<string> GetPageNames()
        {
            return pageNames;
        }

        private static void GetFiles()
        {
            pages = new List<FileInfo>();
            pageNames = new List<string>();
            DirectoryInfo d = new DirectoryInfo(Settings.PageObjectsPath);
            if (!d.Exists)
            {
                Logger.WritePostponedError("Pages folder was not found");
                return;
            }
            FileInfo[] Files = d.GetFiles($"*{Settings.PagesExtension}");
            string str = "";
            foreach (FileInfo file in Files)
            {
                pages.Add(file);

                var pageName = Path.GetFileNameWithoutExtension(file.FullName);

                pageNames.Add(pageName);
            }
        }
    }
}
