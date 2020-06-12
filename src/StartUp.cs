using System;
using System.Threading;
using TestWebFast.ActionController;
using TestWebFast.CommandCenter;
using TestWebFast.Handlers;
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
            CheckData();
            protocol = new Protocol();
            commandManager = new CommandManager(protocol);
            actionProvider = new ActionProvider(commandManager);
            testManager = new TestManager(actionProvider);
        }

        private static void CheckData()
        {
            var errors = new Verificator().CheckDataLocation().GetErrors;

            if (errors.Count > 0)
            {
                Logger.WriteRealTimeError(String.Join("\n ", errors));
                Exit();
            }
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
