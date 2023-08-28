using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement
{
    internal class EmployeeOperation
    {
        private SqlConnection con;
        private void connection()
        {
            string connectionstr = "data source = (localdb)\\MSSQLLocalDB; initial catalog = EmployeeManagement; integrated security = true";
            con = new SqlConnection(connectionstr);
        }
        public bool Addemployeee(Employee obj)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("AddEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Name", obj.Name);
                com.Parameters.AddWithValue("@City", obj.City);
                com.Parameters.AddWithValue("@Address", obj.Address);
                con.Open();
                int i = com.ExecuteNonQuery(); //Execute and return the num of records added
                con.Close();
                if (i != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public bool deletemployeee(int EmpId)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("DeleteEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", EmpId);
                con.Open();
                int i = com.ExecuteNonQuery(); //Execute and return the num of records added
                con.Close();
                if (i != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public bool UpdateEmployee(Employee obj)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("UpdateEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", obj.EmpId);
                com.Parameters.AddWithValue("@Name", obj.Name);
                com.Parameters.AddWithValue("@City", obj.City);
                com.Parameters.AddWithValue("@Address", obj.Address);
                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void GetAllEmployee()
        {
            connection();
            List<Employee> emplist = new List<Employee>();
            SqlCommand com = new SqlCommand("GetAllEmployee", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                emplist.Add(
                    new Employee
                    {
                        EmpId = Convert.ToInt32(dr["EmpId"]),
                        Name = Convert.ToString(dr["Name"]),
                        City = Convert.ToString(dr["City"]),
                        Address = Convert.ToString(dr["Address"])
                    }
                    );
            }
            foreach (var data in emplist)
            {
                Console.WriteLine(data.EmpId + " " + data.Name + " " + data.City + " " + data.Address);
            }
        }
    }
}
