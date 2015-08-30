using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio
{
    public class DBConnectData
    {
        public string server {get; set;}
        public string database {get; set;}
        public string user {get; set;}
        public string password { get; set; }
        public string port { get; set; }

        public DBConnectData()
        {
            server = Properties.Settings.Default.DBHost;
            port = Properties.Settings.Default.DBPort;
            user = Properties.Settings.Default.DBUser;
            password = Properties.Settings.Default.DBPass.DecryptString().ToInsecureString();
            database = Properties.Settings.Default.DBBase;
        }

    }
}
