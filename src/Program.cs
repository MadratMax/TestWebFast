using System.Runtime.InteropServices.ComTypes;
using TestWebFast.ActionController;

namespace TestWebFast
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu.ShowInitApp();
            StartUp.InitializeApp();
            StartUp.RunApplication(true);
        }
    }
}