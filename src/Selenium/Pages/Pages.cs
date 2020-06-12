using TestWebFast.Selenium.Pages;

namespace TestWebFast
{
    using System.Collections.Generic;
    using System.Linq;

    public class Pages : IPages
    {
        private readonly List<IPage> pages;

        public Pages()
        {
            this.pages = new List<IPage>();
        }

        public void AddPage(IPage page)
        {
            pages.Add(page);
        }

        public List<IPage> GetPages()
        {
            return this.pages;
        }

        public IPage GetPage(string pageName)
        {
            return pages.FirstOrDefault(n => n.Name == pageName);
        }

        public bool IsPageExist(string pageName)
        {
            return pages.Any(n => n.Name == pageName);
        }

        public T Get<T>()
            where T : Page
        {
            var page = this.GetPages().FirstOrDefault(n => n is T);

            var instance = (T)page;

            return instance;
        }
    }
}