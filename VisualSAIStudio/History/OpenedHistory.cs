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
        private List<OpenedHistoryAction> history = new List<OpenedHistoryAction>();
        private static OpenedHistory instance;

        public delegate void DelgateMethod(OpenedHistoryAction action, int num);

        OpenedHistory()
        {
            if (System.IO.File.Exists("data/history.json"))
            {
                string file = System.IO.File.ReadAllText("data/history.json");
                history = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OpenedHistoryAction>>(file);
            }
        }

        private void save()
        {
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(history);
            System.IO.File.WriteAllText("data/history.json", data);
        }

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
            history.Add(action);
            // TODO: truncate history to X elements..
            save();
        }

        public void InvokeMethod(Delegate method, int limit = 3)
        {
            int newLimit = history.Count - limit;
            if (newLimit < 0)
                newLimit = 0;

            for (int i = newLimit; i < history.Count; ++i)
            {
                method.DynamicInvoke(history[i], newLimit - i);
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
