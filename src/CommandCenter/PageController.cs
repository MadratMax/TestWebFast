using System;
using System.Collections.Generic;
using System.Linq;
using TestWebFast.Selenium.Pages;

namespace TestWebFast.CommandCenter
{
    class PageController
    {
        private Dictionary<string, List<string>> pageData;
        private IPages pages;

        public PageController(Dictionary<string, List<string>> pages)
        {
            this.pageData = pages;
            InitializePages();
        }

        private void InitializePages()
        {
            pages = new Pages();

            foreach (var page in pageData)
            {
                IPage newPage = new Page(page.Key);
                pages.AddPage(newPage);
                AddPageElements(page.Key);
                AddPageTitle(page.Key);
                AddPageUrl(page.Key);
            }

            WebPageConsumer.Pages = pages;
        }

        public void AddPageTitle(string pageName)
        {
            var body = pageData[pageName];

            foreach (var entry in body)
            {
                if (entry.ToLower().Contains("title"))
                {
                    var title = entry.Split(':')[1];
                    IPage first = null;
                    foreach (var p in pages.GetPages())
                    {
                        if (p.Name == pageName)
                        {
                            first = p;
                            break;
                        }
                    }

                    if (first != null) first.Title = title;
                }
            }
        }

        public void AddPageUrl(string pageName)
        {
            var body = pageData[pageName];

            foreach (var entry in body)
            {
                if (entry.ToLower().Contains("url"))
                {
                    var url = entry.Split(':')[1] + ":" + entry.Split(':')[2];

                    IPage first = null;
                    foreach (var p in pages.GetPages())
                    {
                        if (p.Name == pageName)
                        {
                            first = p;
                            break;
                        }
                    }

                    if (first != null) first.Url = url;
                }
            }
        }

        public void AddPageElements(string pageName)
        {
            var body = pageData[pageName];

            foreach (var entry in body)
            {
                if (entry.Contains("element"))
                {
                    var locator = entry.Split(':')[3];
                    var identificator = entry.Split(':')[2];
                    var element = entry.Split(':')[1];
                    IPage first = null;
                    foreach (var p in pages.GetPages())
                    {
                        if (p.Name == pageName)
                        {
                            first = p;
                            break;
                        }
                    }

                    if (first != null) first.Elements.AddElement(element, identificator, locator);
                }
            }
        }

        public void FindPageElement(string element)
        {

        }

        public Elements GetPageElements(string pageName)
        {
            return pages.GetPages().FirstOrDefault(p => p.Name == pageName).Elements;
        }

        public IPage GetPage(string pageName)
        {
            return pages.GetPages().FirstOrDefault(p => p.Name == pageName);
        }

        public List<string> GetPageNames()
        {
            return pageData.Keys.ToList();
        }

        public string GetPageTitle(string pageName)
        {
            return pages.GetPages().FirstOrDefault(p => p.Title == pageName).Title;
        }
    }
}
