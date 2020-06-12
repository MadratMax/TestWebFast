using TestWebFast.Selenium.Pages;

namespace TestWebFast
{
    using OpenQA.Selenium;

    public interface IPage
    {
        string Name { get; set; }

        string Title { get; set; }

        string Url { get; set; }

        Elements Elements { get; }

        bool IsDisplayed(BasePage page, IWebElement element);
    }
}