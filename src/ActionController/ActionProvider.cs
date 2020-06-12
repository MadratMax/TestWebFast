using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using TestWebFast.CommandCenter;
using TestWebFast.TestCenter;

namespace TestWebFast.ActionController
{
    class ActionProvider : IActionProvider
    {
        private TestRunner runner;
        private ICommandManager commandManager;
        private ObjectExtractor extractor;
        private Dictionary<string, Dictionary<int, ActionModel>> actionsHub;

        public ActionProvider(ICommandManager commandManager)
        {
            this.runner = new TestRunner();
            this.commandManager = commandManager;
            this.extractor = new ObjectExtractor();
            this.actionsHub = new Dictionary<string, Dictionary<int, ActionModel>>();
        }

        public void BuildActions(Dictionary<string, List<string>> cases)
        {
            foreach (KeyValuePair<string, List<string>> entry in cases)
            {
                var testName = entry.Key;
                var steps = entry.Value;
                AddActionsToHub(testName, steps);
            }
        }

        public void AddActionsToHub(string testName, List<string> steps)
        {
            var actionsDict = new Dictionary<int, ActionModel>();

            int i = 1;

            foreach (var step in steps)
            {
                var command = commandManager.DefineAction(step);
                var obj = extractor.Extract(testName, step, command);

                actionsDict.Add(i++, new ActionModel(step, command, obj));
            }

            actionsHub.Add(testName, actionsDict);
        }

        public void ExecuteActions(string testName, TestResults testResults)
        {
            var test = actionsHub[testName];

            foreach (var step in test)
            {
                if (!runner.RunStep(testName, step.Value, testResults))
                {
                    testResults.SetTestFailed(testName);
                    break;
                }
            }

            runner.TerminateSession();
        }
    }
}
