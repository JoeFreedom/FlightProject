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
    public class DBconnection
    {
        public event Message Success;
        public event Message Error;
        private MySqlConnection connection;

        private readonly string host;
        private readonly string database;
        private readonly string port;
        private readonly string username;
        private readonly string pass;
        private readonly string connString;

        public DBconnection()
        {
            using (var file = new StreamReader("dbconnect.cfg"))
            {
                string tempLine;
                while ((tempLine = file.ReadLine()) != null)
                {
                    tempLine = tempLine.Trim();
                    var index = tempLine.IndexOf('=');
                    if (index < 0)
                        continue;
                    var tempSymbols = tempLine.Substring(index + 1);
                    var tempVar = tempLine.Substring(0, index);
                    tempSymbols = tempSymbols.Trim();
                    tempVar = tempVar.Trim();

                    switch (tempVar)
                    {
                        case "host":
                            host = tempSymbols;
                            break;
                        case "database":
                            database = tempSymbols;
                            break;
                        case "port":
                            port = tempSymbols;
                            break;
                        case "username":
                            username = tempSymbols;
                            break;
                        case "pass":
                            pass = tempSymbols;
                            break;
                    }
                }
            }
            connString = "Server=" + host + ";Database=" + database + ";port=" + port + ";User Id=" + username + ";password=" + pass;
        }

        public DBconnection(string host, string database, string port, string username, string pass)
        {
            connString = "Server=" + host + ";Database=" + database + ";port=" + port + ";User Id=" + username + ";password=" + pass;
        }

        public void ConnectDB()
        {
            if (connection != null && connection.Ping())
            {
                Error?.Invoke("Connected to Database already");
                return;
            }
            connection = new MySqlConnection(connString);
            connection.Open();
            if (connection.Ping())
            {
                Success?.Invoke("Connection to Databse successful");
            }
            else
            {
                Error?.Invoke("No connection to Database");
            }
        }

        public async Task ConnectDBAsync()
        {
            if (connection != null && connection.Ping())
            {
                Error?.Invoke("[From async method] Connected to Database already");
                return;
            }
            connection = new MySqlConnection(connString);
            await connection.OpenAsync();
            if (connection.Ping())
            {
                Success?.Invoke("[From async method] Connection successful");
            }
            else
            {
                Error?.Invoke("[From async method] No connection to Database");
            }
        }

        public MySqlDataReader SelectQuery(string sql)
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
                    Error?.Invoke("Query went south");
                    return result;
                }
            
        }

        public async Task<DbDataReader> SelectQueryAsync(string sql)
        {
            if (connection.Ping())
            {
                var command = new MySqlCommand { Connection = connection, CommandText = sql };
                var result = await command.ExecuteReaderAsync();
                if (result != null)
                {
                    Success?.Invoke("[From async method] Query completed");
                    return result;
                }
                else
                {
                    Error?.Invoke("[From async method] Query went south");
                    return result;
                }
            }
            else
            {
                Error?.Invoke("[From async method] No connection to Database");
                return null;
            }
        }

        public int InsertQuery(string sql)
        {
            if (connection.Ping())
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

        public async Task<int> InsertQueryAsync(string sql)
        {
            if (connection.Ping())
            {
                var command = new MySqlCommand { Connection = connection, CommandText = sql };
                var result = await command.ExecuteNonQueryAsync();
                if (result > 0)
                {
                    Success?.Invoke("[From async method] Entry to Database successful");
                }
                else
                {
                    Error?.Invoke("[From async method] Entry to Database unsuccessful");
                }
                return result;
            }
            else
            {
                Error?.Invoke("[From async method] No connection to Database");
                return -1;
            }
        }

        public int UpdateQuery(string sql)
        {
            if (connection.Ping())
            {
                var command = new MySqlCommand { Connection = connection, CommandText = sql };
                var result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    Success?.Invoke("Changes to Database successful");
                }
                else
                {
                    Error?.Invoke("Changes to Database unsuccessful");
                }
                return result;
            }
            else
            {
                Error?.Invoke("Connection to Database unavailable");
                return -1;
            }
        }

        public async Task<int> UpdateQueryAsync(string sql)
        {
            if (connection.Ping())
            {
                var command = new MySqlCommand { Connection = connection, CommandText = sql };
                var result = await command.ExecuteNonQueryAsync();
                if (result > 0)
                {
                    Success?.Invoke("[From async method] Changes to Database successful");
                }
                else
                {
                    Error?.Invoke("[From async method] Changes to Database unsuccessful");
                }
                return result;
            }
            else
            {
                Error?.Invoke("[From async method] Connection to Database unavailable");
                return -1;
            }
        }

        public bool IsConnect()
        {
            return connection.Ping();
        }

        public void Close()
        {
            if (connection.Ping())
            {
                connection.Close();
                Success?.Invoke("Access denied");
            }
            else
            {
                Error?.Invoke("Access to Database closed or doesn't exist");
            }
        }
        public async Task CloseAsync()
        {
            if (connection.Ping())
            {
                await connection.CloseAsync();
                Success?.Invoke("[From async method] Access denied");
            }
            else
            {
                Error?.Invoke("[From async method] Access to Database closed or doesn't exist");
            }
        }
    }
}
