using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualSAIStudio
{
    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            if (System.IO.File.Exists("data/database.json"))
            {
                string file = System.IO.File.ReadAllText("data/database.json");
                 DBConnectData data = Newtonsoft.Json.JsonConvert.DeserializeObject<DBConnectData>(file);
                string connectionString;
                connectionString = "SERVER=" + data.server + ";" + "DATABASE=" +
                data.database + ";" + "UID=" + data.user + ";" + "PASSWORD=" + data.password + ";";

                connection = new MySqlConnection(connectionString);
            }
        }

        //open connection to database
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public MySqlCommand Query(String query)
        {
            return new MySqlCommand(query, connection);
        }

        private static DBConnect instance;
        public static DBConnect GetInstance()
        {
            if (instance == null)
            {
                instance = new DBConnect();
                instance.OpenConnection();
            }

            return instance;
        }
    }
}
