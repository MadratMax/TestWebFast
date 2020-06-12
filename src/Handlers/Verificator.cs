using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestWebFast.Handlers
{
    class Verificator
    {
        private List<string> errors;

        public Verificator()
        {
            this.errors = new List<string>();
        }

        public List<string> GetErrors => this.errors;

        private void AddError(string error)
        {
            errors.Add(error);
        }

        public Verificator CheckDataLocation()
        {
            var pagesExists = Directory.Exists(Settings.PageObjectsPath);
            var testsExists = Directory.Exists(Settings.TestsPath);
            var protocolExists = File.Exists(Settings.ProtocolPath);

            if (testsExists)
            {
                int testFilesCount = Directory.GetFiles(Settings.TestsPath).Length;
                if (testFilesCount < 1)
                    AddError($"There is no test to run. Check test dir: {Settings.TestsPath}");
            }

            if (pagesExists)
            {
                int pageFilesCount = Directory.GetFiles(Settings.PageObjectsPath).Length;
                if (pageFilesCount < 1)
                    AddError($"No pages found. Check page objects dir: {Settings.PageObjectsPath}");
            }

            if (!pagesExists)
                AddError("Pages directory does not exist. Check path in appSettings.json");

            if (!testsExists)
                AddError("Tests directory does not exist. Check path in appSettings.json");

            if (!protocolExists)
                AddError("Protocol was not found. Check path in appSettings.json");

            return this;
        }
    }
}
