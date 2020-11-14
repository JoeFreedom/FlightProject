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
        public DBConnection dBconnection;
        public static string search;
        public static int noNumber; // to be used if IsNumberEntered
        public MainWindow()
        {
            InitializeComponent();
            dBconnection = new DBConnection();
        }
        private void button_Search_Click(object sender, RoutedEventArgs e)
        {
            // TO-DO
            // IsEmpty
            // IsMoreThanOne
            // IfExist
            // how to use more than one parameter when searching
            // pass a success message - successful
            // pass an error message - unsuccessful
            // a proper condition for searching
            // API documentation on the project

            if (search == "" )  // TO-DO not working as intended
            {
                MessageBox.Show("Wrong search", "Error", MessageBoxButton.OK);
                // how to clear search if IsOkClicked
                return;
            }
            else 
            {
                try
                {
                    dBconnection.ConnectDB();
                    search = textBox_Search.Text;
                    var passengerDetails = new PassengerDetails();
                    var selectQuery = $"SELECT * from International_Passport WHERE Last_Name = '{search}'";
                    MySqlDataReader sqlDataReader = dBconnection.SelectQuery(selectQuery);
                    //var selectQuery2 = $"SELECT * from International_Passport WHERE First_Name = '{search}'";
                    // MySqlDataReader sqlDataReader2 = dBconnection.SelectQuery(selectQuery2);
                    while (sqlDataReader.Read())
                    {
                        passengerDetails.textBox_Surname.Text = sqlDataReader.GetValue(1).ToString();
                        passengerDetails.textBox_FirstName.Text = sqlDataReader.GetValue(2).ToString();
                        passengerDetails.textBox_Sex.Text = sqlDataReader.GetValue(3).ToString();
                        var dateBD = (DateTime)sqlDataReader.GetValue(4);
                        passengerDetails.textBox_DateOfBirth.Text = dateBD.ToString("d");
                        passengerDetails.textBox_PassportNum.Text = sqlDataReader.GetValue(5).ToString();
                        passengerDetails.textBox_Nationality.Text = sqlDataReader.GetValue(6).ToString();
                        var dateIS = (DateTime)sqlDataReader.GetValue(7);
                        passengerDetails.textBox_DateIssued.Text = dateIS.ToString("d");
                        var dateEXP = (DateTime)sqlDataReader.GetValue(8);
                        passengerDetails.textBox_DateOfExpiry.Text = dateEXP.ToString("d");
                        passengerDetails.textBox_Visa.Text = sqlDataReader.GetValue(9).ToString();
                        MessageBox.Show("Successful");
                    }
                    passengerDetails.Show();
                    //Close();
                }
                catch
                {
                    MessageBox.Show("Unuccessful");
                }
            }
        }
       

    }
}