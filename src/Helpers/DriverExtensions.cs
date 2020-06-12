namespace TestWebFast
{
    using System;
    using System.Linq;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    public static class DriverExtensions
    {
        public static void GoTo(this IWebDriver driver, string url)
        {
            try
            {
                driver.Url = url;
            }
            catch (WebDriverTimeoutException)
            {
                // catch and ignore
            }
        }

        public static void SwitchToWindow(this IWebDriver driver, string windowHandle)
        {
            driver.WaitForSeconds(1);
            driver.SwitchTo().Window(windowHandle);
            WebPageConsumer.InFrame = false;
        }

        public static void SwitchToLastWindow(this IWebDriver driver)
        {
            driver.WaitForSeconds(1);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            WebPageConsumer.InFrame = false;
        }

        public static void SwitchToTab(this IWebDriver driver, int tabIndex)
        {
            driver.WaitForSeconds(1);

            if (driver.WindowHandles.Count >= tabIndex)
            {
                driver.SwitchTo().Window(driver.WindowHandles[tabIndex - 1]);
                WebPageConsumer.InFrame = false;
            }
        }

        public static void WaitForSeconds(this IWebDriver driver, int seconds)
        {
            var now = DateTime.Now;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.PollingInterval = TimeSpan.FromSeconds(1);
            wait.Until(wd => (DateTime.Now - now) - TimeSpan.FromSeconds(seconds) > TimeSpan.Zero);
        }

        public static bool IsDisplayed(this IWebElement element, int seconds = 3)
        {
            bool displayed = false;

            try
            {
                displayed = element != null && Methods.WaitUntil(() => element.Displayed, seconds);
            }
            catch (StaleElementReferenceException)
            {
                // ignore
            }

            return displayed;
        }

        public static IWebElement SafeClick(this IWebElement element)
        {
            bool found = IsDisplayed(element);

            for (int i = 0; i <= 1; i++)
            {
                try
                {
                    if (found)
                    {
                        WebPageConsumer.CurrentElement = element;
                        element.Click();
                        return element;
                    }
                }
                catch (Exception ex)
                {
                    if (ex is StaleElementReferenceException || ex is WebDriverTimeoutException)
                    {
                        Logger.WriteInfo($"SafeClick method. Attempt {i + 1}. {ex.GetType()} has been thrown. Message: {ex.Message}");
                        found = true;
                    }
                }
            }

            throw new ArgumentException($"Cannot click since element {element.Text} was not found");
        }

        public static void ClickOnElementWithJS(this IWebDriver driver, IWebElement element)
        {
            if (IsDisplayed(element))
            {
                string script = @"arguments[0].click();";
                JS_Executor.ExecuteScript(driver, script, element);
            }
            else
            {
                throw new ArgumentException($"Cannot click since element was not found");
            }
        }

        private static void SwitchToFrame(IWebDriver driver, ISearchEngine engine, IWebElement frameElement)
        {
            if (frameElement == null)
            {
                throw new Exception("iFrame wasn't found");
            }

            Methods.WaitUntil(() => frameElement.Displayed);

            driver.SwitchTo().Frame(frameElement);
        }

        public static void SwitchToFrame(this IWebDriver driver, ISearchEngine engine, int frameIndex = 0)
        {
            try
            {
                driver.SwitchTo().Frame(frameIndex);
            }
            catch
            {
                driver.WaitForSeconds(2);
                driver.SwitchTo().Frame(frameIndex);
            }
        }
    }
}
