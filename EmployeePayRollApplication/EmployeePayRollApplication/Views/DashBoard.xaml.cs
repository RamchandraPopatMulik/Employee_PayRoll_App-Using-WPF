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
        SqlConnection sqlConnection = new SqlConnection(@"Server=DESKTOP-ICFRQNG;Database=Fundoo;User Id=DESKTOP-ICFRQNG/Ramchandra;Password=;TrustServerCertificate=True;Integrated Security=SSPI;");

        public void LoadGrid()
        {
            SqlCommand sqlCommand = new SqlCommand("Select empId,name,profile,Gender,Department,Salary,Start_Date,Notes from EmployeeData",sqlConnection);
            DataTable dataTable = new DataTable();
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            dataTable.Load(sqlDataReader);
            sqlConnection.Close();
            datagrid.ItemsSource=dataTable.DefaultView;
        }
        private void Add_User(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void DeleteEvent(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)datagrid.SelectedItem;
            MessageBoxResult messageBoxResult = MessageBox.Show("Are You Sure ???","Delete Confirmation",MessageBoxButton.YesNo);
            if(messageBoxResult == MessageBoxResult.Yes)
            {
                SqlCommand sqlCommand = new SqlCommand("SPDeleteEmployee", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("empId", dataRowView["empId"]);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                LoadGrid();
            }
        }
        private void EditEvent(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)datagrid.SelectedItem;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Name_txt.Text = dataRowView["name"].ToString();
            mainWindow.GenderMenu.Text = dataRowView["Gender"].ToString();
            mainWindow.Sl_Value.Text = dataRowView["Salary"].ToString();
            string fullDate = dataRowView["Start_Date"].ToString();
            string[] Date = fullDate.Split(' ');
            mainWindow.date.Text = Date[0];
            mainWindow.month.Text = Date[1];
            mainWindow.year.Text = Date[2];
            mainWindow.Notes_txt.Text = dataRowView["Notes"].ToString();
            mainWindow.isUpdate = true;
            mainWindow.EmpId = Convert.ToInt32(dataRowView["empId"]);
            mainWindow.Show();
            this.Close();  
        }
    }
}
