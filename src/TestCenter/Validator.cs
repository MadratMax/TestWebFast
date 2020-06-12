using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestWebFast.TestCenter
{
    class Validator
    {
        private TestManager testManager;
        private List<string> invalidTests;

        public Validator(TestManager testManager)
        {
            this.testManager = testManager;
            this.invalidTests = new List<string>();
            CheckTestSteps();
        }

        public bool IsTestValid(string testName)
        {
            return GetInvalidTest().FirstOrDefault(n => n.Equals(testName)) == null;
        }

        private void CheckTestSteps()
        {
            var cases = testManager.GetAllTestCases();

            foreach (var test in cases)
            {
                if(test.Value.Count == 0)
                    AddInvalidTest(test.Key);
            }
        }

        private void AddInvalidTest(string testName)
        {
            invalidTests.Add(testName);
            LoggerHub.AddNotificationToHub($"Test {testName} has no steps");
        }

        private List<string> GetInvalidTest()
        {
            return invalidTests;
        }
    }
}
