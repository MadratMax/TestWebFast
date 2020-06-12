using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestWebFast.CommandCenter;
using TestWebFast.Selenium.Pages;
using TestWebFast.TestCenter;

namespace TestWebFast
{
    internal class TestManager : ITestManager
    {
        private IActionProvider actionProvider;
        private Validator validator;
        private List<FileInfo> testFiles;
        private List<FileInfo> pageFiles;
        private List<string> testNames;
        private Dictionary<string, List<string>> cases;
        private TestResults testResults;
        private Dictionary<string, bool> markedTests;
        private Dictionary<string, bool> runningTests;
        private Dictionary<string, int> testExecutionCount;
        private int testRunCounter;
        private object locked = new object();

        public TestManager(IActionProvider actionProvider)
        {
            this.actionProvider = actionProvider;
            this.testFiles = TestFiles.GetTestCollection();
            this.testNames = TestFiles.GetTestNames();
            this.pageFiles = new DataProvider().GetPageFiles();
            this.cases = new TestCaseParser(testFiles).GetCases();
            this.testResults = new TestResults(testNames);

            InitializePages();
            InitializeTests();

            this.validator = new Validator(this);
        }

        public Dictionary<string, List<string>> GetAllTestCases()
        {
            return cases;
        }

        public List<FileInfo> GetTestFiles()
        {
            return testFiles;
        }

        public int TestRunCount()
        {
            return testRunCounter;
        }

        public List<string> GetStepsForTest(string testName)
        {
            return GetAllTestCases()[testName];
        }

        public Dictionary<string, bool> GetPassedTests()
        {
            return testResults.Results;
        }

        public bool IsTestExecuted(string test)
        {
            return testExecutionCount[test] > 0;
        }

        public bool IsTestPassed(string test)
        {
            return testResults.Results[test];
        }

        public void MarkTest(string testName)
        {
            if (markedTests[testName])
            {
                markedTests[testName] = false;
            }
            else
            {
                markedTests[testName] = true;
            }
        }

        public bool TestIsRunning(string test)
        {
            lock (locked)
            {
                return runningTests[test];
            }
        }

        public bool IsTestRunInProgress()
        {
            lock (locked)
            {
                var busy = runningTests.Any(tr => tr.Value.Equals(true));
                return busy;
            }
        }

        public void SetTestRunning(string test, bool isRunning)
        {
            runningTests[test] = isRunning;
        }

        public bool IsTestMarked(string test)
        {
            return markedTests[test];
        }

        public List<string> GetTestNames()
        {
            lock (locked)
            {
                return testNames;
            }
        }

        public string GetTestStringResult(string testName)
        {
            if (IsTestPassed(testName))
                return "Passed";

            return "Failed";
        }

        public void ChangeTestStatus(string test, bool status = false)
        {
            if(status == false)
                testResults.SetTestFailed(test);
            else
                testResults.SetTestPassed(test);
        }

        public void RunAllTests()
        {
            var tests = GetTestNames();

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = false;
                Logger.WritePostponedInfo($"Full test run is starting...");
                RunAll(tests);
            }).Start();
        }

        private void RunAll(List<string> tests)
        {
            if (!IsTestRunInProgress())
            {
                Logger.WritePostponedInfo($"Full test run is starting...");

                for (int i = 0; i < tests.Count; i++)
                {
                    if(i == 0)
                        Menu.RestartMenu(this);

                    var test = testNames[i];

                    new Thread(() =>
                    {
                        Thread.CurrentThread.IsBackground = false;
                        Logger.WritePostponedInfo($"test {test} is running");
                        Menu.RestartMenu(this);
                    }).Start();
                    
                    Run(test);
                }
            }
        }

        public void RunTest(string testName)
        {
            if (!IsTestRunInProgress())
            {
                Logger.WritePostponedInfo($"test {testName} is running");

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = false;
                    Run(testName);
                }).Start();
            }
        }

        private void Run(string testName)
        {
            WaitForTestRunfinish();

            if (validator.IsTestValid(testName))
            {
                testRunCounter++;
                testExecutionCount[testName]++;
                SetTestRunning(testName, true);
                actionProvider.ExecuteActions(testName, testResults);
                Logger.WritePostponedInfo($"Test {testName} {GetTestStringResult(testName)}");
                SetTestRunning(testName, false);
                Menu.RestartMenu(this);
            }
        }

        private void InitializeTests()
        {
            markedTests = new Dictionary<string, bool>();
            testExecutionCount = new Dictionary<string, int>();
            runningTests = new Dictionary<string, bool>();

            foreach (var test in testNames)
            {
                testResults.SetTestFailed(test);
                markedTests.Add(test, false);
                testExecutionCount.Add(test, 0);
                runningTests.Add(test, false);
            }

            actionProvider.BuildActions(cases);
        }

        private void WaitForTestRunfinish()
        {
            while (IsTestRunInProgress())
            {
                
            }
        }

        private void InitializePages()
        {
            var pages = new PageCollector(pageFiles).GetPages();
            var pageController = new PageController(pages);
        }
    }
}
