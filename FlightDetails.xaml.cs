using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FlightDetails_CourseProject
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class FlightDetails : Window
    {
        private DBconnection connection;
        string host = "mysql60.hostland.ru";
        string database = "host1323541_itstep2";
        string port = "3306";
        string username = "host1323541_itstep";
        string pass = "269f43dc";
        public FlightDetails()
        {
            InitializeComponent();
            connection = new DBconnection (host, database, port, username, pass);

            connection.ConnectDB();
            string query = $"SELECT Flight_Number FROM Flight_Details WHERE Flight_Number = {MainWindow.search}";  

            //var command = new My { Connection = connection, CommandText = query };
            var result = connection.SelectQuery(query);
            textBox_FlightNum.Text = result[0].ToString();

            // how to pass data from one window to another
            // how to address data from DB to target boxes.
            // add a listbox or view to show available data before sending to desired info
            // choose what to see before moving to the main window.
            // police, security, flight managers etc.
            // how to use join to send data to boxes.
            // interaction between WPF windows (watch)
        }

        

    }
}
