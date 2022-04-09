using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Initializers;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    /// <summary>
    /// A class that represents the opened views history. 
    /// </summary>
    public class History
    {
        private Stack<HistoryElement> history = new Stack<HistoryElement>();
        public void AddToHistory(ViewType type, IViewInitializationParameters data)
        {
            history.Push(new HistoryElement(type, data));
        }

        public (ViewType, IViewInitializationParameters) GetLastView()
        {
            if (history.Count > 0)
            {
                history.Pop();
                var element = history.Pop();
                return (element.Type, element.Data);
            }

            return (ViewType.None, null);
        }
    }
}