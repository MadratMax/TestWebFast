using TestWebFast.Selenium.Pages;

namespace TestWebFast
{
    using System.Collections.Generic;

    public interface IPages
    {
        void AddPage(IPage page);

        bool IsPageExist(string pageName);

        IPage GetPage(string pageName);

        List<IPage> GetPages();

        T Get<T>()
            where T : Page;
    }
}