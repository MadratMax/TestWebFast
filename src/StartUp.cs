using System;
using System.Threading;
using TestWebFast.ActionController;
using TestWebFast.CommandCenter;
using TestWebFast.TestCenter;
using TestWebFast.UI;

namespace TestWebFast
{
    class StartUp
    {
        private static IProtocol protocol;
        private static ICommandManager commandManager;
        private static IActionProvider actionProvider;
        private static ITestManager testManager;

        public static void InitializeApp()
        {
            protocol = new Protocol();
            commandManager = new CommandManager(protocol);
            actionProvider = new ActionProvider(commandManager);
            testManager = new TestManager(actionProvider);
        }

        public static void RunApplication(bool command)
        {
            Thread.Sleep(200);

            while (command)
            {
                object choise = ConsoleMenu.MultipleChoice(true, testManager);

                switch (choise)
                {
                    case UiCommands.Exit:
                    {
                        Exit();
                        command = false;
                    }
                        break;

                    case UiCommands.Restart:
                    {
                        RestartApp();
                    }
                        break;
                }
            }
        }

        public static void RestartUi()
        {
            Menu.RestartMenu(testManager);
        }

        private static void RestartApp()
        {
            Console.Clear();
            Menu.ShowInitApp();
            RunApplication(false);
            Logger.ClearHub();
            Dispose();
            InitializeApp();
            Console.Clear();
            RunApplication(true);
        }

        private static void Dispose()
        {
            protocol = null;
            commandManager = null;
            actionProvider = null;
            testManager = null;
        }

        private static void Exit()
        {
            System.Environment.Exit(1);
        }
    }
}
