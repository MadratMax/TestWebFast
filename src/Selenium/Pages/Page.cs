using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestWebFast.Selenium.Pages
{
    public class Page : IPage
    {
        private Elements elements;

        public Page(string name, string title = "", string url = "")
        {
            this.Name = name;
            this.Title = title;
            this.Url = url;
            this.elements = new Elements(this.Name);
        }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public Elements Elements => elements;

        public string GetElementXPathLocator(string elementName)
        {
            return elements.GetLocator(elementName);
        }

        public string GetElementCSSLocator(string elementName)
        {
            return elements.GetLocator(elementName);
        }

        public bool IsDisplayed(BasePage page, IWebElement element)
        {
            throw new NotImplementedException();
        }
    }
}
