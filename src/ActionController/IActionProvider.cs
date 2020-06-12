using System.Collections.Generic;
using TestWebFast.TestCenter;

namespace TestWebFast
{
    internal interface IActionProvider
    {
        void BuildActions(Dictionary<string, List<string>> cases);

        void AddActionsToHub(string testName, List<string> steps);

        void ExecuteActions(string testName, TestResults testResults);
    }
}
