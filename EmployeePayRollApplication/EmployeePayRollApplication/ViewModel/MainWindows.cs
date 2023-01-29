using EmployeePayRollApplication.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeePayRollApplication.ViewModel
{
    public class MainWindows
    {
        DashBords dashboards = new DashBords();
        public string profile_Link = "";
        public int EmpID;
        public bool IsValid(EmployeeModel model)
        {
            if (model.name == String.Empty)
            {
                MessageBox.Show("Name Field is Required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (model.profile == String.Empty)
            {
                MessageBox.Show("Profile Image is not selected \nPlease Select Profile Image.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (model.Department == String.Empty)
            {
                MessageBox.Show("Department is not Selected \nPlease Select Departments ", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (model.Gender == String.Empty)
            {
                MessageBox.Show("Gender is not Selected \nPlease Select Gender", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (model.Salary == "0")
            {
                MessageBox.Show("Salary cannot be Zero", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (model.Start_Date == String.Empty)
            {
                MessageBox.Show("Please Select Start Date", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        public void InsertEmployee(EmployeeModel employee)
        {

            using (dashboards.connection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SPInsertEmployee", dashboards.connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    dashboards.connection.Open();
                    sqlCommand.Parameters.AddWithValue("@name", employee.name);
                    sqlCommand.Parameters.AddWithValue("@profile", employee.profile);
                    sqlCommand.Parameters.AddWithValue("@Gender", employee.Gender);
                    sqlCommand.Parameters.AddWithValue("@Department", employee.Department);
                    sqlCommand.Parameters.AddWithValue("@Salary", employee.Salary);
                    sqlCommand.Parameters.AddWithValue("@Start_Date", employee.Start_Date);
                    sqlCommand.Parameters.AddWithValue("@Notes", employee.Notes);


                    sqlCommand.ExecuteNonQuery();

                   
                    MessageBox.Show("Data Added Successfully ", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    dashboards.connection.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
            public void UpdateEmployee(EmployeeModel employee)
            {
                using (dashboards.connection)
                {
                    try
                    {
                    SqlCommand sqlCommand = new SqlCommand("SPUpdateEmployee", dashboards.connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    dashboards.connection.Open();
                    sqlCommand.Parameters.AddWithValue("@empId", employee.empId);
                    sqlCommand.Parameters.AddWithValue("@name", employee.name);
                    sqlCommand.Parameters.AddWithValue("@profile", employee.profile);
                    sqlCommand.Parameters.AddWithValue("@Gender", employee.Gender);
                    sqlCommand.Parameters.AddWithValue("@Department", employee.Department);
                    sqlCommand.Parameters.AddWithValue("@Salary", employee.Salary);
                    sqlCommand.Parameters.AddWithValue("@Start_Date", employee.Start_Date);
                    sqlCommand.Parameters.AddWithValue("@Notes",employee.Notes);


                    sqlCommand.ExecuteNonQuery();
                    
                    MessageBox.Show("Data Updated Successfully ", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);

                    dashboards.connection.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

        }
    }

