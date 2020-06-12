using System;
using System.Collections.Generic;
using TestWebFast.CommandCenter;

namespace TestWebFast
{
    class Protocol : IProtocol
    {
        private List<string> openBrowserCommands;
        private List<string> openPageCommands;
        private List<string> openTabCommands;
        private List<string> createTabCommands;
        private List<string> closeTabCommands;
        private List<string> pageTitleCommands;
        private List<string> elementTextCommands;
        private List<string> switchFrameCommands;
        private List<string> closeBrowserCommands;
        private List<string> clickCommands;
        private List<string> visibilityCommands;
        private List<string> textInputCommands;
        private List<string> waitCommands;
        private List<string> stopCommands;

        public Protocol()
        {
            this.openBrowserCommands = new List<string>();
            this.openPageCommands = new List<string>();
            this.openTabCommands = new List<string>();
            this.closeBrowserCommands = new List<string>();
            this.clickCommands = new List<string>();
            this.visibilityCommands = new List<string>();
            this.createTabCommands = new List<string>();
            this.closeTabCommands = new List<string>();
            this.pageTitleCommands = new List<string>();
            this.elementTextCommands = new List<string>();
            this.switchFrameCommands = new List<string>();
            this.textInputCommands = new List<string>();
            this.waitCommands = new List<string>();
            this.stopCommands = new List<string>();

            InitializeProtocol();
        }

        public Dictionary<Commands, List<string>> GetCommands()
        {
            return new Dictionary<Commands, List<string>>
            {
                {Commands.OpenBrowser, openBrowserCommands},
                {Commands.CloseBrowser, closeBrowserCommands},
                {Commands.OpenTab, openTabCommands},
                {Commands.OpenPage, openPageCommands},
                {Commands.Click, clickCommands},
                {Commands.Visible, visibilityCommands},
                {Commands.CreateTab, createTabCommands},
                {Commands.CloseTab, closeTabCommands},
                {Commands.ElementText, elementTextCommands},
                {Commands.SwitchFrame, switchFrameCommands},
                {Commands.PageTitle, pageTitleCommands},
                {Commands.TextInput, textInputCommands},
                {Commands.Wait, waitCommands},
                {Commands.Stop, stopCommands}
            };
        }

        private void InitializeProtocol()
        {
            for (int i = 1; i < 20; i++)
            {
                openBrowserCommands.Add(SettingsManager.GetSettingsValue("actions", "open-browser", $"{i}"));
                openPageCommands.Add(SettingsManager.GetSettingsValue("actions", "open-page", $"{i}"));
                openTabCommands.Add(SettingsManager.GetSettingsValue("actions", "open-tab", $"{i}"));
                closeBrowserCommands.Add(SettingsManager.GetSettingsValue("actions", "close", $"{i}"));
                clickCommands.Add(SettingsManager.GetSettingsValue("actions", "element-click", $"{i}"));
                visibilityCommands.Add(SettingsManager.GetSettingsValue("actions", "visibility", $"{i}"));
                createTabCommands.Add(SettingsManager.GetSettingsValue("actions", "create-tab", $"{i}"));
                closeTabCommands.Add(SettingsManager.GetSettingsValue("actions", "close-tab", $"{i}"));
                elementTextCommands.Add(SettingsManager.GetSettingsValue("actions", "element-text", $"{i}"));
                switchFrameCommands.Add(SettingsManager.GetSettingsValue("actions", "switch-frame", $"{i}"));
                pageTitleCommands.Add(SettingsManager.GetSettingsValue("actions", "page-title", $"{i}"));
                textInputCommands.Add(SettingsManager.GetSettingsValue("actions", "enter-text", $"{i}"));
                waitCommands.Add(SettingsManager.GetSettingsValue("actions", "wait", $"{i}"));
                stopCommands.Add(SettingsManager.GetSettingsValue("actions", "stop", $"{i}"));
            }
        }
    }
}