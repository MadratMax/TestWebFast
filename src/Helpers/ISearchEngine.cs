namespace TestWebFast
{
    using System.Collections.Generic;
    using OpenQA.Selenium;

    public interface ISearchEngine
    {
        IWebElement Find(By by);

        IList<IWebElement> FindAll(By by);

        IWebElement QuickFind(By by, int seconds);

        IList<IWebElement> QuickMultipleFind(By by);

        IWebElement FindElementByName(BasePage page, string elementName);

        IEnumerable<IWebElement> FindElementsByName(BasePage page, string elementName);

        IEnumerable<IWebElement> GetElementsOnPageByTag(string tagName);

        string FindPageUrl(BasePage page);

        bool PageIsDisplayed(BasePage page);
    }
}
