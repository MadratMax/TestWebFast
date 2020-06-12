using System;
using System.Collections.Generic;
using System.Linq;
using TestWebFast.CommandCenter;

namespace TestWebFast
{
    class CommandManager : ICommandManager
    {
        private IProtocol protocol;
        private Dictionary<Commands, List<string>> commandsHub;

        public CommandManager(IProtocol protocol)
        {
            this.protocol = protocol;
            this.commandsHub = protocol.GetCommands();
        }

        public Commands DefineAction(string step)
        {
            var i = 0;
            var actionIndex = new Dictionary<Commands, int>();

            foreach (var act in commandsHub)
            {
                var actionList = GetCommandsList(act.Key);
                actionIndex.Add(act.Key, i);

                foreach (var action in actionList)
                {
                    string[] words = step.Split(' ');

                    if (action != null)
                    {
                        actionIndex[act.Key] = actionIndex[act.Key] + action.IndexAction(words);
                    }
                }
            }

            var notDefined = actionIndex.Values.Distinct().Count() == 1;

            if (notDefined)
                return Commands.Undefined;

            var keyOfMaxValue = actionIndex.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

            return keyOfMaxValue;
        }

        private List<string> GetCommandsList(Commands commands)
        {
            return commandsHub[commands];
        }
    }
}
