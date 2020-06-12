namespace TestWebFast.ActionController
{
    class ActionModel
    {
        private string step;

        public ActionModel(string step, Commands command, object obj)
        {
            this.step = step;
            this.Command = command;
            this.Object = obj;
            this.TestContext = TestContext;
        }

        public Commands Command { get; }

        public object Object { get; }

        public string StepName => step;

        public TestContext TestContext { get; }
    }
}
