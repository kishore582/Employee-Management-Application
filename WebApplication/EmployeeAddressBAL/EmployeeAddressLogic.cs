using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using WebApplication.EmployeeAddressSchema;
using System.Data;

namespace WebApplication.EmployeeAddressBAL
{
    public class EmployeeAddressLogic
    {
        private string connectionString = "server=GOWTHAM;Integrated Security=True;TrustServerCertificate=True;database=employeeDetails;";

        private SqlConnection con;

        public EmployeeAddressLogic()
        {
            con = new SqlConnection(connectionString);
        }

        SqlCommand cmd;
        DataTable dt;

        public string GetUserNameById(int selectedUserId)
        {
            string userName = string.Empty;

            using (SqlCommand cmd = new SqlCommand("SELECT Name FROM Employee_Test WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", selectedUserId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                userName = cmd.ExecuteScalar()?.ToString();
                con.Close();
            }

            return userName;
        }

        public int InsertAddress(EmployeeAddress address)
        {
            //int result = 0;

            using (cmd = new SqlCommand("INSERT INTO AppEmployeeTable (Id, Line1, Line2, Line3, City, PinCode) VALUES (@Id, @Line1, @Line2, @Line3, @City, @PinCode)", con))
            {
                con.Open();

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", address.Id);
                cmd.Parameters.AddWithValue("@Line1", address.Line1);
                cmd.Parameters.AddWithValue("@Line2", address.Line2);
                cmd.Parameters.AddWithValue("@Line3", address.Line3);
                cmd.Parameters.AddWithValue("@City", address.City);
                cmd.Parameters.AddWithValue("@PinCode", address.PinCode);

                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                int result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public EmployeeAddress GetEmployeeAddress(int userId)
        {
            EmployeeAddress address = null;
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM AppEmployeeTable WHERE Id = @UserId", con))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        address = new EmployeeAddress
                        {
                            EmpAddressId = reader.GetInt32(0),
                            Line1 = reader.GetString(2),
                            Line2 = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Line3 = reader.IsDBNull(4) ? null : reader.GetString(4),
                            City = reader.GetString(5),
                            PinCode = reader.GetString(6)
                        };
                    }
                }
            }
            return address;
        }

        public List<EmployeeAddress> GetAddressDetails(int userId)
        {
            List<EmployeeAddress> addressList = new List<EmployeeAddress>();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM AppEmployeeTable WHERE Id = @UserId", con))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                con.Close();
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EmployeeAddress address = new EmployeeAddress
                        {
                            EmpAddressId = reader.GetInt32(0),
                            Line1 = reader.GetString(2),
                            Line2 = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Line3 = reader.IsDBNull(4) ? null : reader.GetString(4),
                            City = reader.GetString(5),
                            PinCode = reader.GetString(6)
                        };
                        addressList.Add(address);
                    }
                }
            }
            return addressList;
        }

        public int DeleteAddress(int addressId)
        {
            try
            {
                if (con.State.Equals(ConnectionState.Closed)) con.Open();
                using (var cmd = new SqlCommand("DELETE FROM AppEmployeeTable WHERE EmpAddressId = @EmpAddressId", con))
                {
                    cmd.Parameters.AddWithValue("@EmpAddressId", addressId);

                    int result = cmd.ExecuteNonQuery();
                    con.Close();
                    return result;
                }
            }

            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                return 0;
            }
        }

        public DataTable GetAllAddresses(int addressId)
        {

            List<EmployeeAddress> addressList = new List<EmployeeAddress>();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM AppEmployeeTable WHERE EmpAddressId = @addressId", con))
            {
                cmd.Parameters.AddWithValue("@addressId", addressId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EmployeeAddress address = new EmployeeAddress
                        {
                            EmpAddressId = reader.GetInt32(0),
                            Line1 = reader.GetString(2),
                            Line2 = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Line3 = reader.IsDBNull(4) ? null : reader.GetString(4),
                            City = reader.GetString(5),
                            PinCode = reader.GetString(6)
                        };
                        addressList.Add(address);
                    }
                }
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpAddressId", typeof(int));
            dt.Columns.Add("Line1", typeof(string));
            dt.Columns.Add("Line2", typeof(string));
            dt.Columns.Add("Line3", typeof(string));
            dt.Columns.Add("City", typeof(string));
            dt.Columns.Add("PinCode", typeof(string));

            foreach (var address in addressList)
            {
                dt.Rows.Add(address.EmpAddressId, address.Line1, address.Line2, address.Line3, address.City, address.PinCode);
            }

            return dt;
        }

        public List<EmployeeAddress> GetAddressesById(int empAddressId)
        {
            List<EmployeeAddress> addresses = new List<EmployeeAddress>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString)) // Define your connection string
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM AppEmployeeTable WHERE EmpAddressId = @EmpAddressId;", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@EmpAddressId", empAddressId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EmployeeAddress address = new EmployeeAddress
                                {
                                    EmpAddressId = Convert.ToInt32(reader["EmpAddressId"]),
                                    Line1 = reader["Line1"].ToString(),
                                    Line2 = reader["Line2"].ToString(),
                                    Line3 = reader["Line3"].ToString(),
                                    City = reader["City"].ToString(),
                                    PinCode = reader["PinCode"].ToString()
                                };
                                addresses.Add(address);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving addresses: " + ex.Message, ex);
            }

            return addresses; 
        }

        public int UpdateAddress(EmployeeAddressSchema.EmployeeAddress objSchema, int EmpAddressId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("UPDATE AppEmployeeTable SET Line1 = @Line1, Line2 = @Line2, Line3 = @Line3, City = @City, PinCode = @PinCode WHERE EmpAddressId = @EmpAddressId", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        // Add parameters for the update query
                        cmd.Parameters.AddWithValue("@EmpAddressId", EmpAddressId);
                        cmd.Parameters.AddWithValue("@Line1", objSchema.Line1);
                        cmd.Parameters.AddWithValue("@Line2", objSchema.Line2);
                        cmd.Parameters.AddWithValue("@Line3", objSchema.Line3);
                        cmd.Parameters.AddWithValue("@City", objSchema.City);
                        cmd.Parameters.AddWithValue("@PinCode", objSchema.PinCode);

                        // Execute the query and return the number of affected rows
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        int result = cmd.ExecuteNonQuery();
                        return result;
                        //return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating address: " + ex.Message, ex);
            }
           
        }
    }
}