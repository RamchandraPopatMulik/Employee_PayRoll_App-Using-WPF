using System;
using System.Collections.Generic;
using System.Data;
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

namespace EmployeePayRollApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public string profile_Link = "";
        public bool isUpdate = false;
        public int EmpId;
        SqlConnection sqlConnection = new SqlConnection(@"Server=DESKTOP-ICFRQNG;Database=Fundoo;User Id=DESKTOP-ICFRQNG/Ramchandra;Password=;TrustServerCertificate=True;Integrated Security=SSPI;");

        public void Clear()
        {
            Name_txt.Clear();
            Notes_txt.Clear();
            a_radio.IsChecked = false;
            b_radio.IsChecked = false;
            c_radio.IsChecked = false;
            d_radio.IsChecked = false;
            GenderMenu.Text = "";
            HR.IsChecked = false;
            Sales.IsChecked = false;
            Finance.IsChecked = false;
            Engineer.IsChecked = false;
            Others.IsChecked = false;
            Salary_Slider.Value = 0;
        }
        public bool IsValid()
        {
            if(Name_txt.Text == String.Empty)
            {
                MessageBox.Show("Name is Required ","Failed",MessageBoxButton.OK,MessageBoxImage.Error);
                return false;
            }
            if (a_radio.IsChecked == false && b_radio.IsChecked ==false && c_radio.IsChecked == false && d_radio.IsChecked==false)
            {
                MessageBox.Show("Profile Image is Required ", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (HR.IsChecked == false && Sales.IsChecked == false && Finance.IsChecked == false && Engineer.IsChecked == false)
            {
                MessageBox.Show("Department is Required ", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (GenderMenu.Text == String.Empty)
            {
                MessageBox.Show("Gender is Required ", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Salary_Slider.Value ==0)
            {
                MessageBox.Show("Salary is Not Zero ", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            DashBoard dashBoard = new DashBoard();
            dashBoard.Show();
            this.Close();
        }

        private void radio_Checked(object sender, RoutedEventArgs e)
        {
            profile_Link = (string)(sender as RadioButton).Content;
        }
        private void Submit_Button_Click(object sender, RoutedEventArgs e)
        {
           if(!isUpdate)
            {
                if (IsValid())
                {
                    string Date = date.Text + " " + month.Text + " " + year.Text;
                    var SelectedItem = Department1.Items.Cast<CheckBox>().Where(x => x.IsChecked == true).Select(x => x.Content).ToList();
                    using (sqlConnection)
                    {
                        try
                        {
                            SqlCommand sqlCommand = new SqlCommand("sp_InsertIntoEmployee", sqlConnection);
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            sqlConnection.Open();
                            sqlCommand.Parameters.AddWithValue("@Name", Name_txt.Text);
                            sqlCommand.Parameters.AddWithValue("@Profile", Profile);
                            sqlCommand.Parameters.AddWithValue("@Gender", GenderMenu.Text);
                            sqlCommand.Parameters.AddWithValue("@Department", string.Join(",",SelectedItem));
                            sqlCommand.Parameters.AddWithValue("@Salary", Sl_Value.Text);
                            sqlCommand.Parameters.AddWithValue("@Date", date);
                            sqlCommand.Parameters.AddWithValue("@Note", Notes_txt.Text);


                            sqlCommand.ExecuteNonQuery();
                            sqlConnection.Close();
                            MessageBox.Show("Data Added Successfully ", "Saved", MessageBoxButton.OK, MessageBoxImage.Error);
                            Clear();
                            DashBoard dashBoard = new DashBoard();
                            dashBoard.Show();
                            this.Close();
                        }
                        catch(SqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }
    }
}
