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
using System.Windows.Shapes;

namespace EmployeePayRollApplication
{
    /// <summary>
    /// Interaction logic for DashBoard.xaml
    /// </summary>
    public partial class DashBoard : Window
    {
        public DashBoard()
        {
            InitializeComponent();
            LoadGrid();
        }
        DashBords dashBords = new DashBords();

        public void LoadGrid()
        {
            datagrid.ItemsSource=dashBords.GetAllEmployee();
        }
        private void Add_User(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void DeleteEvent(object sender, RoutedEventArgs e)
        {
            try
            {
                EmployeeModel data = (EmployeeModel)datagrid.SelectedItem;
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Datete Conformation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {

                    dashBords.DeleteEmployee(data.empId);
                    LoadGrid();

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void EditEvent(object sender, RoutedEventArgs e)
        {
            try
            {
                EmployeeModel employeeModel = (EmployeeModel)datagrid.SelectedItem;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Name_txt.Text = employeeModel.name;
                mainWindow.GenderMenu.Text = employeeModel.Gender;
                mainWindow.Sl_Value.Text = employeeModel.Salary;
                string fullDate = employeeModel.Start_Date;
                string[] Date = fullDate.Split(' ');
                mainWindow.date.Text = Date[0];
                mainWindow.month.Text = Date[1];
                mainWindow.year.Text = Date[2];
                mainWindow.Notes_txt.Text = employeeModel.Notes;
                mainWindow.isUpdate = true;
                mainWindow.EmpId = employeeModel.empId;
                mainWindow.Show();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
