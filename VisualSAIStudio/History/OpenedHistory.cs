using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio.History
{
    public enum RecentType
    {
        Creature,
        Gameobject,
    }

    public struct OpenedHistoryAction
    {
        public RecentType type { set; get; }
        public int entry { set; get; }
        public string name { set; get; }
    }

    class OpenedHistory
    {
        private List<OpenedHistoryAction> list = new List<OpenedHistoryAction>();
        private static OpenedHistory instance;

        public delegate void DelgateMethod(OpenedHistoryAction action, int num);

        public void insert(RecentType type, int entry, string name)
        {
            OpenedHistoryAction action = new OpenedHistoryAction();
            action.type = type;
            action.entry = entry;
            action.name = name;
            insert(action);
        }

        public void insert(OpenedHistoryAction action)
        {
            list.Add(action);
        }

        public void InvokeMethod(Delegate method, int limit = 3)
        {
            int newLimit = list.Count - limit;
            if (newLimit < 0)
                newLimit = 0;
            for (int i = newLimit; i < list.Count; ++i)
            {
                method.DynamicInvoke(list[i], newLimit - i);
            }
        }

        public static OpenedHistory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OpenedHistory();
                }
                return instance;
            }
        }
    }
}
