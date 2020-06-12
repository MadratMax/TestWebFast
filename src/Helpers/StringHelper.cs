namespace TestWebFast
{
    static class StringHelper
    {
        public static int IndexAction(this string definedAction, params string[] stepWords)
        {
            int matchIndex = 0;

            foreach (string word in stepWords)
            {
                var splitDefinedAction  = definedAction.Split(' ');

                foreach (var command in splitDefinedAction)
                {
                    if (command.Contains(word))
                        matchIndex++;
                }
            }

            return matchIndex;
        }
    }
}
