using System;
using System.Collections.Generic;

namespace TestWebFast.CommandCenter
{
    interface IProtocol
    {
        Dictionary<Commands, List<string>> GetCommands();
    }
}
