using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication.EmployeeSchema;

namespace WebApplication.EmployeeDAL
{
    public class Class1
    {
        private string connectionString = "server=GOWTHAM;Integrated Security=True;TrustServerCertificate=True;database=employeeDetails;";

        private SqlConnection con;

        public Class1()
        {
            con = new SqlConnection(connectionString);
        }

        SqlCommand cmd;
        DataTable dt;
        public int InsertData(EmployeeSchema.Class1 objSchema)
        {
            try
            {
                using (cmd = new SqlCommand("INSERT INTO Employee_Test (Name, PhoneNumber, Age) VALUES (@Name, @PhoneNumber, @Age)", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Para", "ADD");
                    cmd.Parameters.AddWithValue("@Name", objSchema.Name);
                    cmd.Parameters.AddWithValue("@PhoneNumber", objSchema.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Age", Convert.ToInt32(objSchema.Age));
                    if (con.State.Equals(ConnectionState.Closed)) con.Open();
                    int result = cmd.ExecuteNonQuery();
                    con.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        public int UpdateData(EmployeeSchema.Class1 objSchema, int Id)
        {
            try
            {
                using (cmd = new SqlCommand("UPDATE Employee_Test SET Name = @Name, PhoneNumber = @PhoneNumber, Age = @Age WHERE Id = @Id;", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Para", "Update");
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@Name", objSchema.Name);
                    cmd.Parameters.AddWithValue("@PhoneNumber", objSchema.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Age", Convert.ToInt32(objSchema.Age));
                    if (con.State.Equals(ConnectionState.Closed)) con.Open();
                    int result = cmd.ExecuteNonQuery();
                    con.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        public int DeleteData(int Id)
        {
            try
            {
                if (con.State.Equals(ConnectionState.Closed)) con.Open();

                using (var checkCmd = new SqlCommand("SELECT COUNT(*) FROM AppEmployeeTable WHERE Id = @Id;", con))
                {
                    checkCmd.Parameters.AddWithValue("@Id", Id);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        int result = 0;
                        return result;
                    }
                }

                using (var cmd = new SqlCommand("DELETE FROM Employee_Test WHERE Id = @Id;", con))
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    int result = cmd.ExecuteNonQuery();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State.Equals(ConnectionState.Open)) con.Close();
            }
        }

        public DataTable BindGrid()
        {
            using (cmd = new SqlCommand("SELECT * FROM Employee_Test", con))
            {
                try
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Para", "Get_For_Grid");
                    if (con.State.Equals(ConnectionState.Closed)) con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable GetById(int Id)
        {
            using (cmd = new SqlCommand("SELECT * FROM Employee_Test WHERE Id = @Id;", con))
            {
                try
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Para", "Get_By_Id");
                    cmd.Parameters.AddWithValue("@Id", Id);
                    if (con.State.Equals(ConnectionState.Closed))
                        con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}