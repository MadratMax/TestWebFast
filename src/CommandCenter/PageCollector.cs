using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestWebFast.CommandCenter
{
    class PageCollector
    {
        private List<FileInfo> pageFiles;
        private Dictionary<string, List<string>> pages;

        public PageCollector(List<FileInfo> pageFiles)
        {
            this.pageFiles = pageFiles;
            this.pages = new Dictionary<string, List<string>>();
            ReadPages();
        }

        public Dictionary<string, List<string>> GetPages()
        {
            return pages;
        }

        private void ReadPages()
        {
            foreach (var testFile in pageFiles)
            {
                List<string> lines = File.ReadAllLines(testFile.FullName).ToList();
                List<string> data = new List<string>();

                for (int i = 0; i < lines.Count; i++)
                {
                    data.Add(lines[i]);
                }

                pages.Add(Path.GetFileNameWithoutExtension(testFile.FullName), data);
            }
        }
    }
}