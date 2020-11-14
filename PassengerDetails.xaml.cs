using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
    /// Interaction logic for PassengerDetails.xaml
    /// </summary>
    public partial class PassengerDetails : Window
    {
        public DBConnection dbconnection;
        public PassengerDetails()
        {
            InitializeComponent();
            dbconnection = new DBConnection();
        }     
        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            // TO-DO 
            // IfPassengerExists
            // 
            dbconnection.ConnectDB();
            var insertQuery = $"INSERT INTO International_Passport(Last_Name, First_Name, Sex, Date_Of_Birth, Passport_Number, Nationality, Date_Issued, Date_Of_Expiry, Visa_Type)" +
                $" VALUES('"+this.textBox_Surname.Text+ "','" + this.textBox_FirstName.Text + "','" + this.textBox_Sex.Text + "','" + this.textBox_DateOfBirth.Text + "','" + this.textBox_PassportNum.Text + "','" + this.textBox_Nationality.Text + "','" + this.textBox_DateIssued.Text + "','" + this.textBox_DateOfExpiry.Text + "','" + this.textBox_Visa.Text + "');";
            MySqlCommand sqlCommand = new MySqlCommand(insertQuery);
            MySqlDataReader dataReader;
            dbconnection.InsertUpdateQuery(insertQuery);
            dataReader = sqlCommand.ExecuteReader();           
            while (dataReader.Read())
            {
                MessageBox.Show("Inserted");
            }
        }
        private void button_Edit_Click(object sender, RoutedEventArgs e)
        {
            dbconnection.ConnectDB();
            var updateQuery = $"UPDATE International_Passport SET Id='" + this.textBlock_id.Text + "'," + 
                "Last_Name='" + this.textBox_Surname.Text + "', " +
                "First_Name='" + this.textBox_FirstName.Text + "', " +
                "Sex='" + this.textBox_Sex.Text + "', " +
                "Date_Of_Birth='" + this.textBox_DateOfBirth.Text + "', " +
                "Passport_Number='" + this.textBox_PassportNum.Text + "', " +
                "Nationality='" + this.textBox_Nationality.Text + "', " +
                "Date_Issued='" + this.textBox_DateIssued.Text + "', " +
                "Date_Of_Expiry='" + this.textBox_DateOfExpiry.Text + "', " +
                "Visa_Type='" + this.textBox_Visa.Text + "') " +
                " WHERE Id='"+ this.textBlock_id.Text +"'";
            // ???how to properly use ID to as the condition without it showing on the passenger window

            MySqlCommand sqlCommand = new MySqlCommand(updateQuery);
            MySqlDataReader dataReader;
            dbconnection.InsertUpdateQuery(updateQuery);
            dataReader = sqlCommand.ExecuteReader();
            while (dataReader.Read())
            {
                MessageBox.Show("Updated");
            }
        }
        private void button_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
