using System;
using System.Threading;
using TestWebFast.TestCenter;
using TestWebFast.UI;

namespace TestWebFast
{
    internal class ConsoleMenu
    {
        public static int CurrentSelection;
        public static int optionsPerLine = Settings.Columns;
        public static int spacingPerLine = Settings.SpacingPerLine;

        public static object MultipleChoice(bool canCancel, ITestManager testManager, params string[] pars)
        {
            var tests = testManager.GetTestNames();

            Console.CursorVisible = true;
            ConsoleKey key;

            do
            {
                Menu.ShowTop();

                Menu.MainMenu(testManager);

                Menu.ShowBottomBar();

                key = Console.ReadKey(true).Key;

                var testName = tests[CurrentSelection];

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            if (CurrentSelection % optionsPerLine > 0)
                                CurrentSelection--;
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            if (CurrentSelection % optionsPerLine < optionsPerLine - 1)
                                CurrentSelection++;
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            if (CurrentSelection >= optionsPerLine)
                                CurrentSelection -= optionsPerLine;
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            if (CurrentSelection + optionsPerLine < tests.Count)
                                CurrentSelection += optionsPerLine;
                            break;
                        }
                    case ConsoleKey.A:
                    {
                        testManager.RunAllTests();
                        break;
                    }
                    case ConsoleKey.I:
                    {
                        testName = tests[CurrentSelection];
                        var steps = testManager.GetStepsForTest(testName);
                        Menu.ShowSteps(testName, steps);
                        break;
                    }
                    case ConsoleKey.Spacebar:
                    {
                        testManager.MarkTest(testName);
                        break;
                    }
                    case ConsoleKey.Enter:
                    {
                        testManager.RunTest(testName);
                    }
                        break;
                    case ConsoleKey.Escape:
                    {
                        if (canCancel)
                        {
                            key = Menu.ShowConfirmToTo("exit");

                            if (key == ConsoleKey.Enter)
                            {
                                return UiCommands.Exit;
                            }
                        }
                        break;
                    }
                    case ConsoleKey.R:
                    {
                        key = Menu.ShowConfirmToTo("restart");

                        if (key == ConsoleKey.Enter)
                        {
                            return UiCommands.Restart;
                        }
                    }
                        break;
                }

            } while (key != ConsoleKey.Escape);

            Console.CursorVisible = true;

            var selectedTest = testManager.GetTestNames()[CurrentSelection];

            return selectedTest;
        }
    }
}