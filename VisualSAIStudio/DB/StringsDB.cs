using DBCViewer;
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
        private Dictionary<int, string> spells = new Dictionary<int,string>();
        private Dictionary<int, string> emotes= new Dictionary<int,string>();
        private Dictionary<int, string> sounds = new Dictionary<int, string>();
        public Dictionary<int, string> creatures= new Dictionary<int,string>();
        public Dictionary<int, string> quests= new Dictionary<int,string>();
        private Dictionary<int, string> items = new Dictionary<int, string>();
        public Dictionary<int, string> movies = new Dictionary<int, string>();
        private Dictionary<int, string> areas = new Dictionary<int, string>();
        private Dictionary<int, string> classes = new Dictionary<int, string>();

        public void LoadAll()
        {
            LoadCreatures();
            LoadQuests();
            LoadDBCIntAndString(emotes, "Emotes.dbc");
            LoadDBCIntAndString(spells, "Spell.dbc", 20);
            LoadDBCIntAndString(sounds, "SoundEntries.dbc", 1);
            LoadDBCIntAndString(items, "item-sparse.db2", 98);
            LoadDBCIntAndString(areas, "AreaTable.dbc", 10);
            LoadDBCIntAndString(classes, "chrClasses.dbc", 2);
            LoadMovies();
        }

        private void LoadMovies()
        {
            LoadDBCIntAndString(movies, "movie.dbc", 0);
            foreach (int key in movies.Keys.ToList())
                movies[key] = movies[key].Substring(movies[key].LastIndexOf("\\") + 1);
        }

        private void LoadQuests()
        {
            MySql.Data.MySqlClient.MySqlCommand cmd = DBConnect.GetInstance().Query("SELECT entry, title FROM quest_template order by entry");
            using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    quests.Add(Convert.ToInt32(reader["entry"]), Convert.ToString(reader["title"]));
                }
            }
        }

        private void LoadCreatures()
        {
            MySql.Data.MySqlClient.MySqlCommand cmd = DBConnect.GetInstance().Query("SELECT entry, name FROM creature_template order by entry");
            using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    creatures.Add(Convert.ToInt32(reader["entry"]), Convert.ToString(reader["name"]));
                }
            }
        }


        private void LoadDBCIntAndString(Dictionary<int, string> dict, String filename, int fields_to_skip = 0)
        {
            if (!File.Exists("dbc\\" + filename))
                return;
            IWowClientDBReader m_reader;
            if (filename.IndexOf(".dbc") > 0)
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
                dict[id] = name;
            }
        }

        private String GetStringOrNull(Dictionary<int, string> dict, int key)
        {
            if (dict.ContainsKey(key))
                return dict[key];
            else
                return null;
        }



        public String GetSpellName(int id)
        {
            return GetStringOrNull(spells, id);
        }

        public string GetEmoteName(int id)
        {
            return GetStringOrNull(emotes, id);
        }

        public string GetCreatureName(int entry)
        {
            return GetStringOrNull(creatures, entry);
        }

        public string GetQuestName(int entry)
        {
            return GetStringOrNull(quests, entry);
        }

        public string GetSoundName(int id)
        {
            return GetStringOrNull(sounds, id);
        }

        public string GetItemName(int id)
        {
            return GetStringOrNull(items, id);
        }

        public String GetMovieName(int id)
        {
            return GetStringOrNull(movies, id);
        }

        public String GetZoneAreaName(int id)
        {
            return GetStringOrNull(areas, id);
        }

        public String GetClassName(int id)
        {
            return GetStringOrNull(classes, id);
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
}
