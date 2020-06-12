namespace TestWebFast
{
    using OpenQA.Selenium;

    class JS_Executor
    {
        private static bool success;

        public static bool Success => success;

        public static object ExecuteScript(IWebDriver driver, string script)
        {
            success = false;
            var result = (object)null;

            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                result = js.ExecuteScript(script);
                success = true;
            }
            catch
            {
                success = false;
            }

            return result;
        }

        public static void ExecuteScript(IWebDriver driver, string script, IWebElement element)
        {
            success = false;

            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript(script, element);
                success = true;
            }
            catch
            {
                success = false;
            }
        }
    }
}