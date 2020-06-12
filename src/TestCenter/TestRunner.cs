using System;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestWebFast.Selenium.Pages;
using TestWebFast.TestCenter;

namespace TestWebFast.ActionController
{
    class TestRunner
    {
        private IWebDriver driver;
        private ISearchEngine engine;
        private bool result;
        private TestTerminator terminator;

        public TestRunner()
        {
            
        }

        public bool RunStep(string testName, ActionModel step, TestResults testResults)
        {
            result = false;

            try
            {
                RunStateMachine(testName, step, testResults);
            }
            catch (Exception e)
            {
                testResults.AddError(testName, e.Message);
            }

            if(result)
                testResults.SetTestPassed(testName);

            this.terminator = new TestTerminator(driver);
            return result;
        }

        public void TerminateSession()
        {
            terminator.DisposeSession();
        }

        private void RunStateMachine(string testName, ActionModel step, TestResults testResults)
        {
            if(step.Object == null)
            {
                testResults.AddError(testName, $"Undefined step: {step.StepName}");
            }

            switch (step.Command)
            {
                case Commands.Undefined:
                {
                    testResults.AddError(testName, $"Undefined step: {step.StepName}");
                    result = false;
                }
                    break;
                case Commands.OpenBrowser:
                {
                    // TODO parse browser string
                    driver = new ChromeDriver();
                    engine = new WebSearchEngine(driver);
                    result = true;
                }
                    break;
                case Commands.Click:
                {
                    if (step.Object is Element)
                    {
                        Element element = (Element)step.Object;
                        IWebElement foundElement = null;

                        if (element.Identificator == "xpath")
                        {
                            foundElement = engine.Find(By.XPath(element.Locator));
                        }
                        if (element.Identificator == "css")
                        {
                            foundElement = engine.Find(By.CssSelector(element.Locator));
                        }
                        if (foundElement == null)
                        {
                            testResults.AddError(testName, $"Element '{element.Name}' was not found");
                            result = false;
                            return;
                        }
                        else
                        {
                            foundElement.SafeClick();
                            WebPageConsumer.CurrentElement = foundElement;
                            result = true;
                            return;
                        }
                    }
                    testResults.AddError(testName, $"Step: {step.StepName}. No matching elements found in page collection");
                    result = false;
                }
                    break;
                case Commands.OpenTab:
                {
                    int tabIndex = (int) step.Object;
                    if (driver.WindowHandles.Count >= tabIndex)
                    {
                        driver.SwitchTo().Window(driver.WindowHandles[tabIndex - 1]);
                    }
                    result = true;
                }
                    break;
                case Commands.OpenPage:
                {
                    IPage page = (IPage)step.Object;
                    if (page == null)
                    {
                        testResults.AddError(testName, $"Step: {step.StepName}. Page was not found");
                        result = false;
                        return;
                    }
                    driver.Url = page.Url;
                    result = true;
                }
                    break;
                case Commands.Visible:
                {
                    if (step.Object is Element)
                    {
                        Element element = (Element)step.Object;
                        IWebElement foundElement = null;

                        if (element.Identificator == "xpath")
                        {
                            foundElement = engine.Find(By.XPath(element.Locator));
                        }
                        if (element.Identificator == "css")
                        {
                            foundElement = engine.Find(By.CssSelector(element.Locator));
                        }
                        if(foundElement == null)
                        {
                            testResults.AddError(testName, $"Element '{element.Name}' was not found");
                            result = false;
                        }
                        result = foundElement.IsDisplayed();
                        WebPageConsumer.CurrentElement = foundElement;
                        return;
                    }
                    result = false;
                }
                    break;
                case Commands.CreateTab:
                {
                    // TODO does not work
                    IWebElement body = driver.FindElement(By.TagName("body"));
                    body.SendKeys(Keys.Control + 't');
                    driver.SwitchToLastWindow();
                    result = true;
                }
                    break;
                case Commands.ElementText:
                {
                    if (step.Object is Element)
                    {
                        Element element = (Element) step.Object;

                        if (element.Identificator == "xpath")
                        {
                            var foundElement = engine.Find(By.XPath(element.Locator));

                            if (foundElement == null)
                            {
                                testResults.AddError(testName, $"Element '{element.Name}' was not found");
                                result = false;
                            }

                            if (foundElement.IsDisplayed())
                            {
                                var actualElementText = foundElement.GetAttribute("value");
                                var opentag = "<";
                                var closeTag = ">";

                                    if (string.IsNullOrEmpty(actualElementText))
                                {
                                    actualElementText = foundElement.Text;
                                }

                                if (step.StepName.Contains(opentag) && step.StepName.Contains(closeTag))
                                {
                                    var placeHolders = Regex.Matches(step.StepName, $"<(.*?)>");

                                    var placeHolder = placeHolders[0].Value;
                                    var expectedText = placeHolder.Substring(1, placeHolder.Length - 2);
                                    if (expectedText == actualElementText)
                                    {
                                        result = true;
                                        WebPageConsumer.CurrentElement = foundElement;
                                        return;
                                    }
                                }
                                else
                                {
                                    LoggerHub.AddNotificationToHub($"Test {testName}. Step: {step.StepName}. Input text is not defined. Please use <> to specify a text");
                                }

                                testResults.AddError(testName,
                                    $"step: {step.StepName}. Actual element text is {actualElementText}");
                                result = false;
                            }
                        }

                        if (element.Identificator == "css")
                        {
                            var foundElement = engine.Find(By.XPath(element.Locator));

                            if (foundElement == null)
                            {
                                testResults.AddError(testName, $"Step: {step.StepName}. Element '{element.Name}' was not found");
                                result = false;
                            }

                            if (foundElement.IsDisplayed())
                            {
                                var actualElementText = foundElement.GetAttribute("value");

                                if (string.IsNullOrEmpty(actualElementText))
                                {
                                    actualElementText = foundElement.Text;
                                }

                                if (step.StepName.Substring(step.StepName.IndexOf(element.Name) + element.Name.Length)
                                    .Contains(actualElementText))
                                {
                                    result = true;
                                    WebPageConsumer.CurrentElement = foundElement;
                                    return;
                                }

                                testResults.AddError(testName,
                                    $"Step: {step.StepName}. Actual element text is {actualElementText}");
                                result = false;
                            }

                            return;
                        }

                        result = false;
                    }
                }
                    break;
                case Commands.PageTitle:
                {
                    result = true;
                }
                    break;
                case Commands.SwitchFrame:
                {
                    result = true;
                }
                    break;
                case Commands.CloseBrowser:
                {
                    driver.Close();
                    //driver.Quit();
                    //driver.Dispose();
                    result = true;
                }
                    break;
                case Commands.TextInput:
                {
                    if (step.Object is string)
                    {
                        if (WebPageConsumer.CurrentElement != null)
                        {
                            WebPageConsumer.CurrentElement.SendKeys($"{step.Object}");
                            result = true;
                            return;
                            // TODO add verification for entered text
                        }

                        testResults.AddError(testName, $"Step: {step.StepName}. Current element is not defined.");
                    }

                    testResults.AddError(testName, $"Step: {step.StepName}. Input text failed.");
                    result = false;
                }
                    break;
                case Commands.Wait:
                {
                    if (step.Object is int)
                    {
                        Methods.WaitForSeconds((int)step.Object);
                        result = true;
                        return;
                    }

                    testResults.AddError(testName, $"Step: {step.StepName}. Failed to extract a waiting time.");
                    result = false;
                }
                    break;
                case Commands.Stop:
                {
                    Logger.WritePostponedInfo($"Test {testName} is paused. Press 'P' to continue.");
                    Menu.RestartMenu();
                    Console.ReadKey();
                    result = true;
                    return;
                }
                    break;
            }
        }
    }
}
