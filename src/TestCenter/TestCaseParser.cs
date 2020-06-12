using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestWebFast
{
    class TestCaseParser
    {
        private List<FileInfo> testFiles;
        private Dictionary<string, List<string>> cases;

        public TestCaseParser(List<FileInfo> testFiles)
        {
            this.testFiles = testFiles;
            this.cases = new Dictionary<string, List<string>>();
            ReadTests();
        }

        public Dictionary<string, List<string>> GetCases()
        {
            return cases;
        }

        private void ReadTests()
        {
            foreach (var testFile in testFiles)
            {
                List<string> lines = File.ReadAllLines(testFile.FullName).ToList();
                List<string> steps = new List<string>();

                for (int i = 0; i < lines.Count; i++)
                {
                    steps.Add(lines[i]);
                }

                cases.Add(Path.GetFileNameWithoutExtension(testFile.FullName), steps);
            }
        }
    }
}
