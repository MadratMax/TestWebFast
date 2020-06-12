using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;

namespace TestWebFast.TestCenter
{
    class TestResults
    {
        private readonly List<string> tests;
        private readonly Dictionary<string, bool> results;
        private Dictionary<string, string> errors;

        public TestResults(List<string> tests)
        {
            this.tests = tests;
            this.results = new Dictionary<string, bool>();
            this.errors = this.tests.ToDictionary(x => x, x => String.Empty);
        }

        public List<string> Tests => tests;

        public Dictionary<string, bool> Results => results;

        public Dictionary<string, string> Errors => errors;

        public void AddError(string testName, string error)
        {
            Errors[testName] = error;
            LoggerHub.TestErrors[testName] = Errors[testName];
        }

        public void AddError(string testName, string stepName, string error)
        {
            Errors[testName] = error;
            LoggerHub.TestErrors[testName] = Errors[testName] + "\n step: " + stepName;
        }

        public void RemoveError(string testName)
        {
            Errors[testName] = string.Empty;
            LoggerHub.RemoveError(testName);
        }

        public string GetError(string testName)
        {
            return Errors[testName];
        }

        public void SetTestPassed(string testName)
        {
            this.results[testName] = true;
            RemoveError(testName);
        }

        public void SetTestFailed(string testName)
        {
            this.results[testName] = false;
        }
    }
}
