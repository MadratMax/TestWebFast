using System;
using System.Collections.Generic;
using System.Linq;
using TestWebFast.TestCenter;

namespace TestWebFast
{
    class Menu
    {
        private static ITestManager testManager;

        public static void ShowTop()
        {
            var error = LoggerHub.PostponedErrors[^1];
            var info = LoggerHub.PostponedInfos[^1];
            var note = LoggerHub.Notification;

            Console.Clear();

            Console.WriteLine($" ");
            Console.WriteLine($" 'A' - run all tests | 'I' - view test steps | 'Space' - mark test | 'R' - restart | 'Esc' - quit\n" +
                              $" ___________________________________________________________________________________________________\n" +
                              $" {info}\n" +
                              $" ___________________________________________________________________________________________________\n" +
                              $" {error}\n" +
                              $" ___________________________________________________________________________________________________\n");
            System.Console.WriteLine("");
        }

        public static void ShowInitApp()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n Initializing. Please wait..");
        }

        public static void MainMenu(ITestManager testManager)
        {
            Menu.testManager = testManager;
            const int startX = 2;
            const int startY = 8;
            int optionsPerLine = ConsoleMenu.optionsPerLine;
            int spacingPerLine = ConsoleMenu.spacingPerLine;
            var tests = testManager.GetTestNames();
            tests.Sort();
            tests.ToArray();

            for (int i = 0; i < tests.Count; i++)
            {
                if (ConsoleMenu.CurrentSelection >= tests.Count)
                {
                    ConsoleMenu.CurrentSelection = tests.Count - 1;
                }

                var selectedString = $"{tests[i]}";
                var slectedTest = selectedString;

                if (selectedString.Length >= 30)
                {
                    selectedString = selectedString.Substring(0, 30) + "...";
                }

                try
                {
                    Console.SetCursorPosition(startX + (i % optionsPerLine) * spacingPerLine, startY + i / optionsPerLine);
                }
                catch (Exception e)
                {
                    Logger.WriteRealTimeError("Failed to set cursor position. Try to change window size");
                    break;
                }

                if (!testManager.IsTestExecuted(tests[i]))
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    if (testManager.TestIsRunning(slectedTest))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }
                    else
                    {
                        if (testManager.IsTestPassed(tests[i]))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                    }
                }

                string pointer;
                if (i == ConsoleMenu.CurrentSelection)
                {
                    pointer = "->";
                }
                else
                {
                    pointer = "  ";
                }

                if (testManager.IsTestMarked(slectedTest))
                {
                    pointer = $"{pointer}*";
                }

                Console.Write($"{pointer} {selectedString}");
                Console.ResetColor();
            }
        }

        public static void ShowBottomBar()
        {
            Console.WriteLine($"\n\n ___________________________________________________________________________________________________");
            var notifications = LoggerHub.GetNotifications().Select(i => i.ToString()).ToArray();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($" {String.Join("\n ", notifications)}");
            Console.ResetColor();
            Console.WriteLine($" ___________________________________________________________________________________________________\n\n");
        }

        public static void ShowSteps(string testName, List<string> steps)
        {
            Console.Clear();
            Console.WriteLine($" ___________________________________________________________________________________________________");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" {testName}");
            Console.ResetColor();
            Console.WriteLine($" ___________________________________________________________________________________________________\n");

            int index = 1;

            foreach (var step in steps)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($" {index++} {step}");
                Console.ResetColor();
            }

            Console.WriteLine($" ___________________________________________________________________________________________________");
            if (!string.IsNullOrEmpty(LoggerHub.GetError(testName)))
            {
                Console.WriteLine($"\n Error:");
                Console.WriteLine($" {LoggerHub.TestErrors[testName]}");
                Console.WriteLine($" ___________________________________________________________________________________________________");
            }
            
            Console.WriteLine(" \n\n\n press any key");

            Console.ReadKey();
        }

        public static ConsoleKey ShowConfirmToTo(string cause)
        {
            Console.Clear();
            Console.WriteLine($"\n ___________________________________________________________________________________________________");
            Console.WriteLine($" press 'Enter' to {cause}");
            Console.WriteLine($" ___________________________________________________________________________________________________");
            var key = Console.ReadKey(true).Key;
            return key;
        }

        public static void ShowInstantFullScreenMessage(string message)
        {
            Console.Clear();
            Console.WriteLine($"\n ___________________________________________________________________________________________________");
            Console.WriteLine($" {message}");
            Console.WriteLine($" ___________________________________________________________________________________________________");
            Console.ReadKey();
        }

        public static void RestartMenu(ITestManager testManager = null)
        {
            if (testManager == null)
                testManager = Menu.testManager;

            ShowTop();
            MainMenu(testManager);
            ShowBottomBar();
        }
    }
}