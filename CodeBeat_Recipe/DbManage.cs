using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace CodeBeat_Recipe
{

    public class DbManage
    {
        private MySqlConnection conn;
        private string m_strServer;
        public string m_strDatabase;
        private string m_strUserid;
        private string m_strPassword;
        public Dictionary<string, string> dict = new Dictionary<string, string>();


        public DbManage()
        {
            this.m_strServer = "127.0.0.1";
            this.m_strUserid = "root";
            this.m_strPassword = "root";
        }

        public DbManage(string Database)
        {
            this.m_strServer = "127.0.0.1";
            this.m_strUserid = "root";
            this.m_strPassword = "root";
            this.m_strDatabase = Database;
        }

        public void make_connection()
        {
            string ConString = "SERVER=" + m_strServer + ";" +
                               //"DATABASE=" + m_strDatabase + ";" +
                               "UID=" + m_strUserid + ";" +
                               "PASSWORD=" + m_strPassword + ";" +
                               "Port = 3306 ;";
            this.conn = new MySqlConnection(ConString);
        }

        public void make_connection(string dbStr)
        {
            this.m_strDatabase = dbStr;
            string ConString = "SERVER=" + m_strServer + ";" +
                               //"DATABASE=" + m_strDatabase + ";" +
                               "UID=" + m_strUserid + ";" +
                               "PASSWORD=" + m_strPassword + ";" +
                               "Port = 3306 ;";
            this.conn = new MySqlConnection(ConString);
        }

        private bool OpenConnection()
        {
            try
            {
                this.conn.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                this.conn.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }


        //Select statement
        public void Select(string qry)
        {

            this.dict.Clear();
            string query = qry;

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {

                        this.dict.Add(dataReader.GetName(i), dataReader.GetValue(i).ToString());
                    }
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

            }
            else
            {

            }
        }



        public string SelectAField(string qry)
        {
            string query = qry;
            string retStr = "";


            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    retStr = dataReader.GetString(0);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return retStr;
            }
            else
            {
                return retStr;
            }
        }



        //Count Statement
        public int Count()
        {
            string query = "SELECT Count(*) FROM model";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //create 
                MySqlCommand cmd = new MySqlCommand(query, conn);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }


        public void SelectMultiple(string qry, ref List<string> obj)
        {
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(qry, conn);

                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    obj.Add(dataReader.GetValue(0).ToString());
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
            }
        }
    }
}
