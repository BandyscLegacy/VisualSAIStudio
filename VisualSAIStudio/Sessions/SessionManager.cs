using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio.Sessions
{
    class SessionManager : IEnumerable<Session>
    {
        private List<Session> sessions = new List<Session>();

        SessionManager()
        {
            if (System.IO.File.Exists("data/sessions.json"))
            {
                string file = System.IO.File.ReadAllText("data/sessions.json");
                sessions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Session>>(file);
            }
        }

        public void Save()
        {
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(sessions);
            System.IO.File.WriteAllText("data/sessions.json", data);
        }

        public void Add(Session session)
        {
            sessions.Add(session);
            Save();
        }

        public void Remove(Session session)
        {
            sessions.Remove(session);
            Save();
        }

        public void RemoveSingle(Session session, OpenedScratch openedScratch)
        {
            session.list.Remove(openedScratch);
            Save();
        }

        public void Rename(Session session, string title)
        {
            int index = sessions.IndexOf(session);
            sessions.Remove(session);
            session.title = title;
            sessions.Insert(index, session);
            Save();
        }

        public IEnumerator<Session> GetEnumerator()
        {
            foreach (Session session in sessions)
                yield return session;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        private static SessionManager instance;
        public static SessionManager GetInstance()
        {
            if (instance == null)
                instance = new SessionManager();
            return instance;
        }


    }
}
