using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using MySql.Data.MySqlClient;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Common;
using Renci.SshNet.Messages.Authentication;
using Google.Protobuf.WellKnownTypes;

namespace FlightDetails_CourseProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DBConnection dbconnection;
        public static string search;
        public static int noNumber; // to be used if IsNumberEntered
        public MainWindow()
        {
            InitializeComponent();
            dbconnection = new DBConnection();
        }
        private void button_Search_Click(object sender, RoutedEventArgs e)
        {
            // TO-DO
            // IsEmpty
            // IsMoreThanOne
            // IfExist
            // how to use more than one parameter when searching
            // where to output search result if more than one same surnames are found
            // pass an error message - unsuccessful
            // API documentation on the project
            dbconnection.ConnectDB();
            var passengerDetails = new PassengerDetails();
            search = textBox_Search.Text;

            if (search == "" ) 
            {
                MessageBox.Show("Wrong search", "Error", MessageBoxButton.OK); // how to clear search if IsOkClicked
                return;
            }
            else
            {  
                var selectQuery = $"SELECT * from International_Passport WHERE Last_Name = '{search}'";
                MySqlDataReader sqlDataReader = dbconnection.SelectQuery(selectQuery);

                while (sqlDataReader.Read()) //you can use "if" to prevent repeated search "until additional window created"
                {
                    passengerDetails.textBox_Surname.Text = sqlDataReader.GetValue(1).ToString();
                    passengerDetails.textBox_FirstName.Text = sqlDataReader.GetValue(2).ToString();
                    passengerDetails.textBox_Sex.Text = sqlDataReader.GetValue(3).ToString();
                    try  
                    { 
                        var dateBD = (DateTime)sqlDataReader.GetValue(4);
                        passengerDetails.datepickerBD.Text = dateBD.ToString("d");
                    }
                    catch
                    {
                        passengerDetails.datepickerBD.Text = "nil";
                    }
                    passengerDetails.textBox_PassportNum.Text = sqlDataReader.GetValue(5).ToString();
                    passengerDetails.textBox_Nationality.Text = sqlDataReader.GetValue(6).ToString();
                    try
                    { 
                        var dateIS = (DateTime)sqlDataReader.GetValue(7);
                        passengerDetails.datePickerIssued.Text = dateIS.ToString("d");
                    }
                    catch
                    {
                        passengerDetails.datePickerIssued.Text = "nil";
                    }
                    try
                    {
                        var dateEXP = (DateTime)sqlDataReader.GetValue(8);
                        passengerDetails.datePickerExpiry.Text = dateEXP.ToString("d");
                    }
                    catch 
                    { 
                        passengerDetails.datePickerExpiry.Text = "nil";
                    }
                        
                    passengerDetails.textBox_Visa.Text = sqlDataReader.GetValue(9).ToString();
                    passengerDetails.textBox_id.Text = sqlDataReader.GetValue(0).ToString();
                    MessageBox.Show("Successful");
                }
                passengerDetails.Show();
              //Close();
            }
        }
    }
}