using System.Linq;
using OpenQA.Selenium;

namespace TestWebFast
{
    internal static class WebPageConsumer
    {
        private static IPage currentPage;

        public static IPage CurrentPage
        {
            get => GetCurrentPage();
            set => currentPage = value;
        }

        public static string CurrentPageName => CurrentPage.GetPageName();

        public static IPages Pages { get; set; }

        public static IWebElement CurrentElement { get; set; }

        public static bool InFrame { get; set; }

        public static IPage GetPage(string pageName)
        {
            return Pages.GetPage(pageName);
        }

        public static string GetPageName(this IPage page)
        {
            return page.Name;
        }

        private static IPage GetCurrentPage()
        {
            if(Pages.GetPages().FirstOrDefault() == null)
                Logger.WriteRealTimeError($"No pages found. Check page objects dir: {Settings.PageObjectsPath}");

            return currentPage?? Pages.GetPages()?.FirstOrDefault();
        }
    }
}