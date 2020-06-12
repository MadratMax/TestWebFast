using OpenQA.Selenium;

namespace TestWebFast.TestCenter
{
    class TestTerminator
    {
        private IWebDriver driver;

        public TestTerminator(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void DisposeSession()
        {
            this.driver?.Quit();
            this.driver?.Dispose();
            Methods.KillChromeDriver();
        }
    }
}
