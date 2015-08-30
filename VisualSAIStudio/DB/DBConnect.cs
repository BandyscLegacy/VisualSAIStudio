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
        private DBConnectData connectionData;

        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            connectionData = new DBConnectData();
            if (!String.IsNullOrEmpty(connectionData.user))
            {
                MakeNewConnection();
            }
        }

        private void MakeNewConnection()
        {
            string connectionString;
            connectionString =  "SERVER="   + connectionData.server     + ";" +
                                "PORT="     + connectionData.port       + ";" +
                                "DATABASE=" + connectionData.database   + ";" +
                                "UID="      + connectionData.user       + ";" +
                                "PASSWORD=" + connectionData.password   + ";";
            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        public bool OpenConnection()
        {
            if (connection == null)
                return false; // no db settings

            try
            {
                connection.Open();
                return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                /*switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server. Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }*/
                MessageBox.Show(ex.Message);
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
            if (connection == null)
                return null;// no db settings
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
