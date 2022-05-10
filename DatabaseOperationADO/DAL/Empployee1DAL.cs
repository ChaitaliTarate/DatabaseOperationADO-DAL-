using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseOperationADO.DAL;
using DatabaseOperationADO.Model;

namespace DatabaseOperationADO.DAL
{
    public class Employee1DAL
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public Employee1DAL()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }

        public DataTable GetAllEmployee1()
        {
            DataTable table = new DataTable();
            string qry = "select * from Employee1";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            table.Load(dr);
            con.Close();


            return table;
        }

        public Employee1 GetEmployee1ById(int id)
        {
            Employee1 employee1 = new Employee1();
            string qry = "select * from Employee1 where EmpId=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    employee1.EmpId = Convert.ToInt32(dr["EmpId"]);
                    employee1.EmpName = dr["EmpName"].ToString();
                    employee1.Designation = dr["Designation"].ToString();
                    employee1.Salary = Convert.ToDouble(dr["Salary"]);
                }
            }
            con.Close();
            return employee1;
        }


        public int Save(Employee1 employee1)
        {
            string qry = "insert into Employee1 values(@id,@name,@designation,@salary)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", employee1.EmpId);
            cmd.Parameters.AddWithValue("@name", employee1.EmpName);
            cmd.Parameters.AddWithValue("@designation", employee1.Designation);
            cmd.Parameters.AddWithValue("@salary", employee1.Salary);
           
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();

            return res;
        }

        public int Update(Employee1 employee1)
        {
            string qry = "update Employee1 set EmpName=@name,Designation=@designation,Salary=@salary where EmpId=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", employee1.EmpName);
            cmd.Parameters.AddWithValue("@designation", employee1.Designation);
            cmd.Parameters.AddWithValue("@salary", employee1.Salary);
            cmd.Parameters.AddWithValue("@id", employee1.EmpId);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();

            return res;

        }

        public int Delete(int id)
        {
            string qry = "delete from Employee1 where EmpId=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id",id);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();

            return res;
        }
    }


}
