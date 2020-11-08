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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightDetails_CourseProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void Message(string message);
        
        public static string search;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void button_Search_Click(object sender, RoutedEventArgs e)
        {
            // var temp = connection.SelectQuery(textBox_Search.Text);
            var dbDetails = new ClassDatabaseDetails();
            search = textBox_Search.Text;
            var flightDetails = new FlightDetails();
            dbDetails.FlightNum = flightDetails.textBox_FlightNum.Text;
            flightDetails.Show()
            var passengerDetails = new PassengerDetails();
            passengerDetails.Show();


        }
      
    }
}