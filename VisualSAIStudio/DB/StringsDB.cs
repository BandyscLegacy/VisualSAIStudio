using DBCViewer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio
{
    public class StringsDB
    {
        private Dictionary<StorageType, ClientData> database = new Dictionary<StorageType, ClientData>();

        public EventHandler CurrentAction = delegate { };

        public void LoadAll()
        {
            CurrentAction(this, new LoadingEventArgs("spells"));
            database.Add(StorageType.Spell, new ClientDataDBC("spell.dbc", 20));

            CurrentAction(this, new LoadingEventArgs("quests"));
            database.Add(StorageType.Quest, new ClientDataDB("title", "quest_template"));

            CurrentAction(this, new LoadingEventArgs("quests"));
            database.Add(StorageType.Creature, new ClientDataDB("entry", "name", "creature_template"));

            CurrentAction(this, new LoadingEventArgs("gameobjects"));
            database.Add(StorageType.GameObject, new ClientDataDB("entry", "name", "gameobject_template"));

            CurrentAction(this, new LoadingEventArgs("items"));
            database.Add(StorageType.Item, new ClientDataDBC("item-sparse.db2", 98));

            CurrentAction(this, new LoadingEventArgs("sounds"));
            database.Add(StorageType.Sound, new ClientDataDBC("SoundEntries.dbc", 1));

            CurrentAction(this, new LoadingEventArgs("movies"));
            database.Add(StorageType.Movie, new ClientDataDBC("movie.dbc", 0));

            CurrentAction(this, new LoadingEventArgs("classes"));
            database.Add(StorageType.Class, new ClientDataDBC("chrClasses.dbc", 2));

            CurrentAction(this, new LoadingEventArgs("areas"));
            database.Add(StorageType.Area, new ClientDataDBC("AreaTable.dbc", 10));

            CurrentAction(this, new LoadingEventArgs("emotes"));
            database.Add(StorageType.Emote, new ClientDataDBC("Emotes.dbc", 0));

            CurrentAction(this, new LoadingEventArgs("skills"));
            database.Add(StorageType.Skill, new ClientDataDBC("SkillLine.dbc", 1));

            CurrentAction(this, new LoadingEventArgs("events and actions"));
            LoadEventsAndActions();
        }

        private void LoadEventsAndActions()
        {
            string data;
            if (File.Exists("data/custom_actions.json"))
            {
                data = File.ReadAllText("data/custom_actions.json");
                List<SmartGenericJSONData> smart_generics = JsonConvert.DeserializeObject<List<SmartGenericJSONData>>(data);
                smart_generics.ForEach(e => SmartFactory.AddAction(e));
            }
            if (File.Exists("data/custom_events.json"))
            {
                data = File.ReadAllText("data/custom_events.json");
                List<SmartGenericJSONData> smart_generics = JsonConvert.DeserializeObject<List<SmartGenericJSONData>>(data);
                smart_generics.ForEach(e => SmartFactory.AddEvent(e));
            }
        }

        private void LoadMovies()
        {
            //LoadDBCIntAndString(movies, "movie.dbc", 0);
            ///foreach (int key in movies.Keys.ToList())
            //    movies[key] = movies[key].Substring(movies[key].LastIndexOf("\\") + 1);
        }

        public string Get(StorageType storage, int id)
        {
            return database[storage].GetString(id);
        }

        public bool Exists(StorageType storage, int id)
        {
            return database[storage].IdExists(id);
        }

        public Dictionary<int, string> GetDictionary(StorageType storage)
        {
            return database[storage].GetDictionary();
        }

        private static StringsDB instance;
        public static StringsDB GetInstance()
        {
            if (instance == null)
            {
                instance = new StringsDB();
                instance.LoadAll();
            }
            return instance;
        }
    }

    public class LoadingEventArgs : EventArgs
    {
        public string loading { get; set; }
        public LoadingEventArgs (string loading)
        {
            this.loading = loading;
        }
    }


    public enum StorageType
    {
        Spell,
        Quest,
        Creature,
        GameObject,
        Item,
        Sound,
        Movie,
        Class,
        Area,
        Emote,
        Skill,
    }

    public abstract class ClientData
    {
        protected Dictionary<int, string> values = new Dictionary<int, string>();

        public bool IdExists(int id)
        {
            return values.ContainsKey(id);
        }

        public string GetString(int id)
        {
            if (values.ContainsKey(id))
                return values[id];
            else
                return null;
        }

        public  Dictionary<int, string> GetDictionary()
        {
            return values;
        }
    }

    public class ClientDataDB : ClientData
    {
        public ClientDataDB(string id_column, string string_column, string table)
        {
            Load(id_column, string_column, table);
        }

        private void Load(string id_column, string string_column, string table)
        {
            try
            {
                MySql.Data.MySqlClient.MySqlCommand cmd = DBConnect.GetInstance().Query(String.Format("SELECT {0}, {1} FROM {2} order by {0}", id_column, string_column, table));
                using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        values.Add(Convert.ToInt32(reader[id_column]), Convert.ToString(reader[string_column]));
                    }
                }
            }
            catch (System.InvalidOperationException e)
            {
                // TODO: show config option to configure sql connection
            }
        }

        public ClientDataDB(string string_column, string table)
        {
            try
            {
                MySql.Data.MySqlClient.MySqlCommand cmd = DBConnect.GetInstance().Query(String.Format("SHOW columns FROM {0}", table));
                MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                string id_column = (string)reader["field"];
                reader.Close();
                Load(id_column, string_column, table);
            }
            catch (System.InvalidOperationException e)
            {
                // TODO: show config option to configure sql connection
            }
        }
    }

    public class ClientDataDBC : ClientData
    {
        public ClientDataDBC(string filename, int fields_to_skip)
        {
            if (!File.Exists("dbc\\" + filename))
                return;
            IWowClientDBReader m_reader;
            if (filename.Contains(".dbc"))
                m_reader = new DBCReader(@"dbc\" + filename);
            else
                m_reader = new DB2Reader(@"dbc\" + filename);

            for (int i = 0; i < m_reader.RecordsCount; ++i)
            {
                BinaryReader br = m_reader[i];
                int id = br.ReadInt32();
                for (int j = 0; j < fields_to_skip; ++j)
                    br.ReadInt32();
                string name = m_reader.StringTable[br.ReadInt32()];
                values[id] = name;
            }
        }
    }

}
