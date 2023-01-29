using EmployeePayRollApplication.Model;
using EmployeePayRollApplication.ViewModel;
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
        MainWindows mainWindows = new MainWindows();
        public bool isUpdate = false;
        public int EmpId;


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
        public EmployeeModel Data()
        {
            string Date = date.Text + " " + month.Text + " " + year.Text;
            var SelectedItem = Department1.Items.Cast<CheckBox>().Where(x => x.IsChecked == true).Select(x => x.Content).ToList();

            EmployeeModel model = new EmployeeModel()
            {
                empId = EmpId,
                name = Name_txt.Text,
                profile = mainWindows.profile_Link,
                Gender = GenderMenu.Text,
                Department = string.Join(",", SelectedItem),
                Salary = Salary_Slider.Value.ToString(),
                Start_Date = Date,
                Notes = Notes_txt.Text,
            };
            return model;
        }


        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            DashBoard dashBoard = new DashBoard();
            dashBoard.LoadGrid();
            dashBoard.Show();
            this.Close();
        }

        private void radio_Checked(object sender, RoutedEventArgs e)
        {
            mainWindows.profile_Link = (string)(sender as RadioButton).Content;
        }
        private void Submit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!isUpdate)
            {
                var empModel = Data();
                if (mainWindows.IsValid(empModel))
                {

                    try
                    {
                        mainWindows.InsertEmployee(empModel);
                        Clear();
                        DashBoard dashBoard = new DashBoard();
                        dashBoard.LoadGrid();
                        dashBoard.Show();
                        this.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                
            }
            else
            {
                var empModel = Data();
                if (mainWindows.IsValid(empModel))
                {

                    try
                    {
                        mainWindows.UpdateEmployee(empModel);
                        Clear();
                        DashBoard dashBoard = new DashBoard();
                        dashBoard.LoadGrid();
                        dashBoard.Show();
                        this.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
        }

    }
}

