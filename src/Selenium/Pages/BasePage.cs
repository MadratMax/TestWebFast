namespace TestWebFast
{
    using OpenQA.Selenium;

    public abstract class BasePage
    {
        public virtual string Url { get; set; }
        public string Name { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public string Title { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public virtual bool IsDisplayed(IPage page, IWebElement element)
        {
            WebPageConsumer.CurrentPage = page;

            return element != null && element.Displayed;
        }
    }
}