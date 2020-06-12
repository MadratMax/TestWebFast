using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TestWebFast.Selenium.Pages;

namespace TestWebFast
{
    class ObjectExtractor
    {

        public ObjectExtractor()
        {

        }

        public object Extract(string testName, string step, Commands command)
        {
            var objArray = step.Split(' ');

            foreach (var entry in objArray)
            {
                if (command == Commands.Click || command == Commands.ElementText || command == Commands.Visible)
                {
                    if (WebPageConsumer.CurrentPage.Elements.IsElementExist(entry))
                    {
                        return WebPageConsumer.CurrentPage.Elements.GetElement(entry);
                    }
                }
                if (command == Commands.CloseTab || command == Commands.OpenTab)
                {
                    int tabIndex;
                    bool isTabIndex = int.TryParse(entry, out tabIndex);

                    if(isTabIndex)
                        return tabIndex;
                }
                if (command == Commands.CreateTab)
                {
                    return entry;
                }
                if (command == Commands.OpenPage || command == Commands.PageTitle)
                {
                    if (WebPageConsumer.Pages.IsPageExist(entry))
                    {
                        WebPageConsumer.CurrentPage = WebPageConsumer.GetPage(entry);
                        return WebPageConsumer.GetPage(entry);
                    }
                }
                if (command == Commands.CloseBrowser || command == Commands.OpenBrowser)
                {
                    return entry;
                }
                if (command == Commands.TextInput)
                {
                    var opentag = "<";
                    var closeTag = ">";

                    if (entry.Contains(opentag) && entry.Contains(closeTag))
                    {
                        var placeHolders = Regex.Matches(entry, $"<(.*?)>");
                        var placeHolder = placeHolders[0].Value;
                        var test = placeHolder.Substring(1, placeHolder.Length - 2);
                        return test;
                    }
                }
                if (command == Commands.Wait)
                {
                    int delayTime;
                    bool isObjectInteger = int.TryParse(entry, out delayTime);

                    if (isObjectInteger)
                        return delayTime;
                }
                if (command == Commands.Stop)
                {
                    return "";
                }
            }

            LoggerHub.AddNotificationToHub($"Test: {testName}. Invalid step: {step}");
            if (command == Commands.TextInput)
            {
                LoggerHub.AddNotificationToHub($"Test: {testName}. Invalid step: {step}. Input text is not defined. please use <> to specify a text");
            }

            return null;
        }
    }
}
