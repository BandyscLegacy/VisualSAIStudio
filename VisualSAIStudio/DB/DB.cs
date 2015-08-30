using DBCViewer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualSAIStudio
{
    public class DB
    {
        private Dictionary<StorageType, ClientData<string>> dbString = new Dictionary<StorageType, ClientData<string>>();
        private Dictionary<StorageType, ClientData<int>> dbInt = new Dictionary<StorageType, ClientData<int>>();
        public event EventHandler CurrentAction = delegate { };
        public event EventHandler FinishedLoading = delegate { };

        public void LoadAll()
        {

            CheckForDBCSettings();

            CurrentAction(this, new LoadingEventArgs("actions"));
            Load(SmartScripts.SmartType.SMART_ACTION, "data/actions.json");

            CurrentAction(this, new LoadingEventArgs("events"));
            Load(SmartScripts.SmartType.SMART_EVENT, "data/events.json");

            CurrentAction(this, new LoadingEventArgs("targets"));
            Load(SmartScripts.SmartType.SMART_TARGET, "data/targets.json");

            CurrentAction(this, new LoadingEventArgs("conditions"));
            Load(SmartScripts.SmartType.SMART_CONDITION, "data/conditions.json");

            CurrentAction(this, new LoadingEventArgs("spells"));
            dbString.Add(StorageType.Spell, new ClientDataDBC("spell.dbc", 20));

            CurrentAction(this, new LoadingEventArgs("quests"));
            dbString.Add(StorageType.Quest, new ClientDataDB<string>("title", "quest_template"));

            CurrentAction(this, new LoadingEventArgs("creatures"));
            dbString.Add(StorageType.Creature, new ClientDataDB<string>("entry", "name", "creature_template"));
            dbString.Add(StorageType.CreatureEntryWithAI, new ClientDataDB<string>("entry", "ScriptName", "creature_template", "AIName = \"SmartAI\""));

            //dbInt.Add(StorageType.CreatureGuid, new ClientDataDB<int>("guid", "entry", "creature"));



            /*MySql.Data.MySqlClient.MySqlCommand cmd = DBConnect.GetInstance().Query(String.Format("SELECT guid, creature.id, map, position_x, position_z, position_y, count(source_type) as smartAI FROM creature left join smart_scripts on source_type=0 and entryorguid=(-guid) group by guid "));
            using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    GuidEntry guid = new GuidEntry();
                    guid.id = Convert.ToInt32(reader["guid"]);
                    guid.map = Convert.ToInt32(reader["map"]);
                    guid.position_x = (float)Convert.ToDouble(reader["position_x"]);
                    guid.position_y = (float)Convert.ToDouble(reader["position_y"]);
                    guid.position_z = (float)Convert.ToDouble(reader["position_z"]);
                    guid.smartAI = (Convert.ToInt32(reader["smartAI"])>0);

                    int entry = Convert.ToInt32(reader["id"]);

                    if (!EntryToGuidCreature.ContainsKey(entry))
                        EntryToGuidCreature[entry] = new List<GuidEntry>();
                    EntryToGuidCreature[entry].Add(guid);
                }
            }*/

            CurrentAction(this, new LoadingEventArgs("gameobjects"));
            dbString.Add(StorageType.GameObject, new ClientDataDB<string>("entry", "name", "gameobject_template"));
            dbString.Add(StorageType.GameObjectEntryWithAI, new ClientDataDB<string>("entry", "ScriptName", "gameobject_template", "AIName = \"SmartGameObjectAI\""));

            CurrentAction(this, new LoadingEventArgs("zones and areas"));
            dbString.Add(StorageType.Area, new ClientDataDBC("AreaTable.dbc", 10));

            CurrentAction(this, new LoadingEventArgs("maps"));
            dbString.Add(StorageType.Map, new ClientDataDBC("Map.dbc", 0));

            CurrentAction(this, new LoadingEventArgs("area triggers"));
            dbString.Add(StorageType.AreaTrigger, new ClientDataDBC("AreaTrigger.dbc", 
                    delegate(BinaryReader br,Dictionary<int, string> strings )
                    {
                        StringBuilder sb = new StringBuilder();
                        br.ReadInt32();
                        sb.Append("(");
                        sb.Append(br.ReadSingle());
                        sb.Append(", ");
                        sb.Append(br.ReadSingle());
                        sb.Append(", ");
                        sb.Append(br.ReadSingle());
                        sb.Append(")");
                        return sb.ToString();
                    }            
                ));
            dbString.Add(StorageType.AreaTriggerWithSAI, new ClientDataDB<string>("entry", "ScriptName", "areatrigger_scripts", "ScriptName = \"SmartTrigger\""));

            CurrentAction(this, new LoadingEventArgs("game events"));
            dbString.Add(StorageType.GameEvent, new ClientDataDB<string>("EventEntry", "description", "game_event"));

            CurrentAction(this, new LoadingEventArgs("items"));
            dbString.Add(StorageType.Item, new ClientDataDBC("item-sparse.db2", 98));

            CurrentAction(this, new LoadingEventArgs("sounds"));
            dbString.Add(StorageType.Sound, new ClientDataDBC("SoundEntries.dbc", 1));


            CurrentAction(this, new LoadingEventArgs("movies"));
            dbString.Add(StorageType.Movie, new ClientDataDBC("movie.dbc", 0));

            CurrentAction(this, new LoadingEventArgs("classes"));
            dbString.Add(StorageType.Class, new ClientDataDBC("chrClasses.dbc", 2));

            CurrentAction(this, new LoadingEventArgs("races"));
            dbString.Add(StorageType.Race, new ClientDataDBC("chrRaces.dbc", 10));

            CurrentAction(this, new LoadingEventArgs("achievement"));
            dbString.Add(StorageType.Achievement, new ClientDataDBC("achievement.dbc", 3));

            CurrentAction(this, new LoadingEventArgs("creature types"));
            dbString.Add(StorageType.CreatureType, new ClientDataDBC("CreatureType.dbc", 0));

            CurrentAction(this, new LoadingEventArgs("phases"));
            dbString.Add(StorageType.Phase, new ClientDataDBC("Phase.dbc", 0));

            CurrentAction(this, new LoadingEventArgs("emotes"));
            dbString.Add(StorageType.Emote, new ClientDataDBC("Emotes.dbc", 0));

            CurrentAction(this, new LoadingEventArgs("skills"));
            dbString.Add(StorageType.Skill, new ClientDataDBC("SkillLine.dbc", 1));

            FinishedLoading(this, new EventArgs());
        }

        private void CheckForDBCSettings()
        {
            if ((Properties.Settings.Default.DBC == "" || !Directory.Exists(Properties.Settings.Default.DBC))
                            && !Properties.Settings.Default.IgnoreMissingDBC)
            {
                DialogResult res =
                  PSTaskDialog.cTaskDialog.ShowTaskDialogBox("DBC",
                                            "Missing DBC",
                                            "Visual SAI Studio makes big use of DBC (client database).",
                                            "",
                                            "",
                                            "Don't check for DBC any more",
                                            "",
                                            "Locate WoW DBC Folder|Ignore for now",
                                            PSTaskDialog.eTaskDialogButtons.Cancel,
                                            PSTaskDialog.eSysIcons.Question, PSTaskDialog.eSysIcons.Information);
                if (PSTaskDialog.cTaskDialog.VerificationChecked)
                    Properties.Settings.Default.IgnoreMissingDBC = true;
                if (PSTaskDialog.cTaskDialog.CommandButtonResult == 0)
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        Properties.Settings.Default.DBC = fbd.SelectedPath;
                    }
                }
                Properties.Settings.Default.Save();
            }
        }

        private void Load(SmartScripts.SmartType type, string file)
        {
            if (File.Exists(file))
            {
                List<SmartGenericJSONData> smart_generics = JsonConvert.DeserializeObject<List<SmartGenericJSONData>>(File.ReadAllText(file));
                smart_generics.ForEach(e => SmartScripts.SmartFactory.GetInstance().Add(type, e));
            }
        }

        public string GetString(StorageType storage, int id)
        {
            return dbString[storage].Get(id);
        }

        public int GetInt(StorageType storage, int id)
        {
            return dbInt[storage].Get(id);
        }


        public bool Exists(StorageType storage, int id)
        {
            return dbString[storage].Contains(id);
        }

        public Dictionary<int, string> GetStringDictionary(StorageType storage)
        {
            if (dbString.ContainsKey(storage))
                return dbString[storage].GetDictionary();
            return null;
        }

        public Dictionary<int, int> GetIntDictionary(StorageType storage)
        {
            if (dbInt.ContainsKey(storage))
                return dbInt[storage].GetDictionary();
            return null;
        }


        private static DB instance;
        public static DB GetInstance()
        {
            if (instance == null)
                instance = new DB();
            return instance;
        }

        public bool Contains(StorageType storageType, int id)
        {
            return dbString[storageType].Contains(id);
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
        GameEvent,
        Race,
        Achievement,
        Map,
        CreatureType,
        Phase,
        CreatureEntryWithAI,
        GameObjectEntryWithAI,
        CreatureGuid,
        AreaTrigger,
        AreaTriggerWithSAI
    }


    public abstract class ClientData<T> : IEnumerable<int>
    {
        protected Dictionary<int, T> values = new Dictionary<int, T>();

        public bool Contains(int id)
        {
            return values.ContainsKey(id);
        }

        public T Get(int id)
        {
            if (values.ContainsKey(id))
                return values[id];
            else
                return default(T);
        }

        public void Set(int id, T value)
        {
            if (values.ContainsKey(id))
                values[id] = value;
            else
                values.Add(id, value);
        }

        public  Dictionary<int, T> GetDictionary()
        {
            return values;
        }

        public IEnumerator<int> GetEnumerator()
        {
            foreach (int key in values.Keys.ToList())
                yield return key;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ClientDataDB<T> : ClientData<T>
    {

        public ClientDataDB(string id_column, string string_column, string table)
        {
            Load(id_column, string_column, table, null);
        }

        private void Load(string id_column, string string_column, string table, string where)
        {
            try
            {
                DBConnect db = new DBConnect();
                db.OpenConnection();
                MySql.Data.MySqlClient.MySqlCommand cmd = db.Query(String.Format("SELECT {0}, {1} FROM {2} {3}order by {0}", id_column, string_column, table, where == null ? "" : "WHERE " + where+" "));

                using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T value = default(T);
                        if (typeof(T) == typeof(string))
                            value = (T)(object)Convert.ToString(reader[string_column]);
                        else if (typeof(T) == typeof(int))
                            value = (T)(object)Convert.ToInt32(reader[string_column]);
                        values.Add(Convert.ToInt32(reader[id_column]), value);
                    }
                }
                db.CloseConnection();
            }
            catch (System.InvalidOperationException /*e*/)
            {
            }
        }

        public ClientDataDB(string string_column, string table)
        {
            string id_column = null;
            try
            {
                MySql.Data.MySqlClient.MySqlCommand cmd = DBConnect.GetInstance().Query(String.Format("SHOW columns FROM {0}", table));
                MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                id_column = (string)reader["field"];
                reader.Close();
            }
            catch (System.InvalidOperationException e)
            {
                // TODO: show config option to configure sql connection
            }
            Load(id_column, string_column, table, null);
        }

        public ClientDataDB(string id_column, string string_column, string table, string where)
        {
            Load(id_column, string_column, table, where);
        }
    }

    public class ClientDataDBC : ClientData<string>
    {
        public ClientDataDBC(string filename, int fields_to_skip)
        {
            Load(filename,
                delegate(BinaryReader br,Dictionary<int, string> strings )
                {
                    string name = "";
                    if (fields_to_skip > 0)
                    {
                        for (int j = 0; j < fields_to_skip; ++j)
                            br.ReadInt32();
                        name = strings[br.ReadInt32()];
                    }
                    return name;
                }
            );
        }
        public ClientDataDBC(string filename, Func<BinaryReader,Dictionary<int, string>, string> func)
        {
            Load(filename, func);
        }
        private void Load(string filename, Func<BinaryReader, Dictionary<int, string>, string> func)
        {
            if (!File.Exists(Properties.Settings.Default.DBC + "\\" + filename))
                return;
            IWowClientDBReader m_reader;
            if (filename.Contains(".dbc"))
                m_reader = new DBCReader(Properties.Settings.Default.DBC + "\\" + filename);
            else
                m_reader = new DB2Reader(Properties.Settings.Default.DBC + "\\" + filename);

            for (int i = 0; i < m_reader.RecordsCount; ++i)
            {
                BinaryReader br = m_reader[i];
                int id = br.ReadInt32();
                values[id] = func(br, m_reader.StringTable);
            }
        }
    }

}
