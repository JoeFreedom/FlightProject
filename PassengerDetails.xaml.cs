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
        private void button_Add_Save_Click(object sender, RoutedEventArgs e)
        {
            // TO-DO 
            // IfPassengerExists
            // how to insert date TYPE into DB
            dbconnection.ConnectDB();
            var selectQuery = $"SELECT Passport_Number FROM International_Passport";
            MySqlDataReader sqlDataReader = dbconnection.SelectQuery(selectQuery);
            if (sqlDataReader.Read())
            {
                MessageBox.Show("A passenger with the same details already exists");
                return;
            }
            else
            {
                var insertQuery = $"INSERT INTO International_Passport(Last_Name, First_Name, Sex, Date_Of_Birth, Passport_Number, Nationality, Date_Issued, Date_Of_Expiry, Visa_Type)" +
                $" VALUES('" + this.textBox_Surname.Text + "','" + this.textBox_FirstName.Text + "','" + this.textBox_Sex.Text + "','" + datepickerBD.SelectedDate.Value.Date.ToShortDateString() + "','" + this.textBox_PassportNum.Text + "','" + this.textBox_Nationality.Text + "','" + datePickerIssued.SelectedDate.Value.Date.ToShortDateString() + "','" + datePickerExpiry.SelectedDate.Value.Date.ToShortDateString() + "','" + this.textBox_Visa.Text + "');";
                MySqlCommand sqlCommand = new MySqlCommand(insertQuery);
                var count = dbconnection.InsertUpdateQuery(insertQuery);
                if (count == 1)
                {
                    MessageBox.Show("Inserted successfully");
                }
                else
                {
                    MessageBox.Show("Not inserted");
                }
            }
        }
        private void button_Edit_Click(object sender, RoutedEventArgs e)
        {
            // ???how do I make data unenable for edit, until edit is clicked
            dbconnection.ConnectDB();
            var updateQuery = $"UPDATE International_Passport SET Last_Name='" + this.textBox_Surname.Text + "', " +
                "First_Name='" + this.textBox_FirstName.Text + "', " +
                "Sex='" + this.textBox_Sex.Text + "', " +
                "Date_Of_Birth='" + datepickerBD.SelectedDate.Value.Date.ToShortDateString() + "', " +
                "Passport_Number='" + this.textBox_PassportNum.Text + "', " +
                "Nationality='" + this.textBox_Nationality.Text + "', " +
                "Date_Issued='" + datePickerIssued.SelectedDate.Value.Date.ToShortDateString() + "', " +
                "Date_Of_Expiry='" + datePickerExpiry.SelectedDate.Value.Date.ToShortDateString() + "', " +
                "Visa_Type='" + this.textBox_Visa.Text + "'" +
                " WHERE Id=" + this.textBox_id.Text ;
            // ???how to properly use Database ID as the condition without it showing on the passenger window
            // to display edit data in the DB
            MySqlCommand sqlCommand = new MySqlCommand(updateQuery);
            var count = dbconnection.InsertUpdateQuery(updateQuery);
            if (count == 1)
            {
                MessageBox.Show("Updated successfully");
            }
            else
            {
                MessageBox.Show("Not updated");
            }   
        }
        // no delete method =  because only authorised personnels are allowed to do that.
        private void button_Close_Click(object sender, RoutedEventArgs e)
        {
             Close();  
        }
    }
}
