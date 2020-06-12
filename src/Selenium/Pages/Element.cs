using System;
using System.Collections.Generic;
using System.Text;

namespace TestWebFast.Selenium.Pages
{
    public class Element
    {
        private string name;
        private string locator;
        private string locatorIdentifier;
        public Element(string name, string locator, string locatorIdentifier)
        {
            this.name = name;
            this.locator = locator;
            this.locatorIdentifier = locatorIdentifier;
        }

        public string Name => name;

        public string Locator => locator;

        public string Identificator => locatorIdentifier;
    }
}
