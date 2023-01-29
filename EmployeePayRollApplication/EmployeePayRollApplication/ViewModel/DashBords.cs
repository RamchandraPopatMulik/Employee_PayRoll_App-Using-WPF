using EmployeePayRollApplication.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeePayRollApplication.ViewModel
{
    public class DashBords
    {
        public SqlConnection connection = new SqlConnection(@"Server=DESKTOP-ICFRQNG;Database=Fundoo;User Id=DESKTOP-ICFRQNG/Ramchandra;Password=;TrustServerCertificate=True;Integrated Security=SSPI;");
        public List<EmployeeModel> GetAllEmployee()
        {
            try
            {
                List<EmployeeModel> employeelist = new List<EmployeeModel>();

                SqlCommand command = new SqlCommand("Select empId,name,profile,Gender,Department,Salary,Start_Date,Notes from EmployeeData", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        employeelist.Add(new EmployeeModel()
                        {
                            name = reader.IsDBNull("name") ? string.Empty : reader.GetString("name"),
                            empId = reader.IsDBNull("empId") ? 0 : reader.GetInt32("empId"),
                            profile = reader.IsDBNull("profile") ? string.Empty : reader.GetString("profile"),
                            Gender = reader.IsDBNull("Gender") ? string.Empty : reader.GetString("Gender"),
                            Department = reader.IsDBNull("Department") ? string.Empty : reader.GetString("Department"),
                            Salary = reader.IsDBNull("Salary") ? string.Empty : reader.GetString("Salary"),
                            Start_Date = reader.IsDBNull("Start_Date") ? string.Empty : reader.GetString("Start_Date"),
                            Notes = reader.IsDBNull("Notes") ? string.Empty : reader.GetString("Notes"),
                        });
                    }
                }
                connection.Close();
                return employeelist;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void DeleteEmployee(int EmpID)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SPDeleteEmployee", connection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                sqlCommand.Parameters.AddWithValue("empId",EmpID);
                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}
