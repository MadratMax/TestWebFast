using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestWebFast.Selenium.Pages
{
    public class Elements
    {
        private string nativePageName;
        private List<Element> elements;
        private Element element;

        public Elements(string nativePageName)
        {
            this.nativePageName = nativePageName;
            this.elements = new List<Element>();
        }

        public string NativePageName => nativePageName;

        public void AddElement(string element, string identificator, string locator)
        {
            if (identificator.ToLower().Contains("xpath"))
            {
                elements.Add(new Element(element, locator, "xpath"));
            }

            if (identificator.ToLower().Contains("css"))
            {
                elements.Add(new Element(element, locator, "css"));
            }
        }

        public string GetLocator(string elementName)
        {
            return elements.FirstOrDefault(n => n.Name == elementName)?.Locator;
        }

        public Element GetElement(string elementName)
        {
            return elements.FirstOrDefault(n => n.Name == elementName);
        }

        public bool IsElementExist(string elementName)
        {
            foreach (var element in elements)
            {
                if (element.Name == elementName)
                    return true;
            }

            return false;
        }
    }
}
