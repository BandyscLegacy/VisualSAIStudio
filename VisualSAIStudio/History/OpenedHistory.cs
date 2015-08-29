using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSAIStudio.SmartScripts;

namespace VisualSAIStudio.History
{

    public struct OpenedHistoryAction
    {
        public SAIType type { set; get; }
        public int entry { set; get; }
        public string name { set; get; }
        public OpenedHistoryAction(SAIType type, int entry, string name) : this()
        {
            this.type = type;
            this.entry = entry;
            this.name = name;
        }
    }

    class OpenedHistory : IEnumerable<OpenedHistoryAction>
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

        public void insert(SAIType type, int entry, string name)
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
            if (history.Count > 10)
                history.RemoveAt(0);
            save();
        }

        public void InvokeMethod(Delegate method, int limit = 3)
        {
            for (int i = 0; i < history.Count; ++i)
            {
                method.DynamicInvoke(history[i], history.Count-i);
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

        public IEnumerator<OpenedHistoryAction> GetEnumerator()
        {
            foreach (OpenedHistoryAction action in history)
                yield return action;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
