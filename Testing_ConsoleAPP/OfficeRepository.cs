using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Testing_ConsoleAPP.DTOs;

namespace Testing_ConsoleAPP
{
    public class OfficeRepository : IOfficeRepository
    {
        string connectionString = "Server=DESKTOP-MOEUPLV\\SQLEXPRESS;Database=myDataBase;Trusted_Connection=True;";

        public bool DeleteEmployee(int FId)
        {
            List<OfficeTableModel> officeTableModels = new List<OfficeTableModel>();

            // SQL query to delete data
            string deleteQuery = "DELETE FROM office_table WHERE F_Id = @FId";

            // Create connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create command
                SqlCommand command = new SqlCommand(deleteQuery, connection);

                // Add parameter for the Id value
                command.Parameters.AddWithValue("@Id", FId);

                try
                {
                    // Open connection
                    connection.Open();

                    // Execute the delete query
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if rows were affected
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Successfully deleted {rowsAffected} row(s).");
                    }
                    else
                    {
                        Console.WriteLine("No rows deleted.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    // Close connection
                    connection.Close();
                }
            }

            return true;
        }

        public List<OfficeTableModel> GetAllEmployees()
        {
            List<OfficeTableModel> employees = new List<OfficeTableModel>();
            // SQL query to execute
            string query = "select * from office_table";


            // Create connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create command
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    // Open connection
                    connection.Open();

                    // Execute the query and get the data
                    SqlDataReader reader = command.ExecuteReader();

                    // Check if there are rows
                    if (reader.HasRows)
                    {
                        // Loop through the rows and display data
                        while (reader.Read())
                        {
                            OfficeTableModel officeTable = new OfficeTableModel
                            {
                                FId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Department = reader.GetString(2),
                            };
                            employees.Add(officeTable);



                            officeTable.PrintDetails();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }

                    // Close reader
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    // Close connection
                    connection.Close();
                }
            }

            return employees;
        }

        public bool CreateEmployee(CreateEmployeeDto createEmployeeDto)
        {
            string insertQuery = "INSERT INTO office_table (F_Id, Name,Department) VALUES (@FId, @Name,@Department)";
            string result = "0";

            // Create connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create command
                SqlCommand command = new SqlCommand(insertQuery, connection);

                // Add parameters
                command.Parameters.AddWithValue("@FId", createEmployeeDto.FId);
                command.Parameters.AddWithValue("@Name", createEmployeeDto.Name);
                command.Parameters.AddWithValue("@Department", createEmployeeDto.Department);

                try
                {
                    // Open connection
                    connection.Open();

                    // Execute the insert query
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if rows were affected
                    if (rowsAffected > 0)
                    {
                        result = "Successfully inserted";
                        Console.WriteLine($"Successfully inserted {rowsAffected} row(s).");
                    }
                    else
                    {
                        Console.WriteLine("No rows inserted.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                //catch for record insered =0

                finally
                {
                    // Close connection
                    connection.Close();
                }
            }

            return true;
        }

        public bool UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {

            // Create connection
            bool anything = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // string updateQuery = "UPDATE office_table SET Name=@Name,Department=@Department WHERE F_Id =@FId;";
                string updateQuery = "UPDATE office_table SET [Name] = IsNull(@Name,[Name]),Department=IsNull(@Department,Department) WHERE F_Id = @FId;";
                string result = "0";
                
                SqlCommand command = new SqlCommand(updateQuery, connection);

                // Add parameters
                command.Parameters.AddWithValue("@FId", updateEmployeeDto.FId);
                //command.Parameters.AddWithValue("@Name", updateEmployeeDto.Name);
                //command.Parameters.AddWithValue("@Department", updateEmployeeDto.Department);
                command.Parameters.AddWithValue("@Name", (updateEmployeeDto.Name != null)? updateEmployeeDto.Name : DBNull.Value);
                command.Parameters.AddWithValue("@Department", (updateEmployeeDto.Department != null)? updateEmployeeDto.Department: DBNull.Value);
                
                try
                {
                    // Open connection
                    connection.Open();

                    // Execute the insert query
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if rows were affected
                    //Transactions
                    if (rowsAffected > 0)
                    {
                        result = "Successfully updated";
                        Console.WriteLine($"Successfully Updated {rowsAffected} row(s).");
                        anything = true;
                    }
                    else
                    {
                        Console.WriteLine("No rows inserted.");
                        anything = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                //catch for record insered =0

                finally
                {
                    // Close connection
                    connection.Close();
                }
            }
            return anything;
        }
    }
}