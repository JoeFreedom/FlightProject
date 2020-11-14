using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace FlightDetails_CourseProject
{
    public delegate void Message(string message);
    public class DBConnection
    {
        public event Message Success;
        public event Message Error;
        private const string HOST = "mysql60.hostland.ru";
        private const string PORT = "3306";
        private const string DATABASE = "host1323541_itstep2";
        private const string USERNAME = "host1323541_itstep";
        private const string PASSWORD = "269f43dc";
        private const string connString = "Server=" + HOST + ";Database=" + DATABASE + ";port=" + PORT + ";User Id=" + USERNAME + ";password=" + PASSWORD;
        private static MySqlConnection connection = new MySqlConnection(connString);
        private static MySqlCommand command = new MySqlCommand();
        public void ConnectDB()
        {
            connection = new MySqlConnection(connString);
            connection.Open();
            if (connection != null)
            {
                Success?.Invoke("Connection successful"); // ??when do I see the success message, not showing
            }
            else
            {
                Error?.Invoke("No connection to Database"); // ??how to know when message not working
            }
        }
        public MySqlDataReader SelectQuery(string sql)
        {
            //connection.Open(); // no need for this, connection already opened
            if (connection != null)
            {
                var command = new MySqlCommand { Connection = connection, CommandText = sql };
                var result = command.ExecuteReader();
                if (result != null)
                {
                    Success?.Invoke("Query completed");
                    return result;
                }
                else
                {
                    Error?.Invoke("Something went wrong");
                    return result;
                }
            }
            else
            {
                Error?.Invoke("No connection to Database");
                return null;
            }
        }
        public int InsertUpdateQuery(string sql)
        {
            if (connection != null)
            {
                var command = new MySqlCommand { Connection = connection, CommandText = sql };
                var result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    Success?.Invoke("Entry to Database successful");
                }
                else
                {
                    Error?.Invoke("Entry to Database unsuccessful");
                }
                return result;
            }
            else
            {
                Error?.Invoke("No connection to Database");
                return -1;
            }
        }
    }  
}
