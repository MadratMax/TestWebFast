using System.Collections.Generic;
using System.IO;

namespace TestWebFast.TestCenter
{
    interface ITestManager
    {
        Dictionary<string, List<string>> GetAllTestCases();

        List<FileInfo> GetTestFiles();

        int TestRunCount();

        List<string> GetStepsForTest(string testName);

        Dictionary<string, bool> GetPassedTests();

        bool IsTestExecuted(string test);

        bool IsTestPassed(string test);

        void MarkTest(string testName);

        bool TestIsRunning(string test);

        bool IsTestRunInProgress();

        void SetTestRunning(string test, bool isRunning);

        bool IsTestMarked(string test);

        List<string> GetTestNames();

        void ChangeTestStatus(string test, bool status = false);

        void RunAllTests();

        void RunTest(string testName);
    }
}
