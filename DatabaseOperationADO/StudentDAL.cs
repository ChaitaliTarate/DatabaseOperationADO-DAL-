using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DatabaseOperationADO
{
   public class StudentDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public StudentDAL()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }

        public int CreateStudent(Student student)
        {
            string qry = "insert into Student values(@roll,@sname,@branch,@percentage)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@roll",student.RollNo);
            cmd.Parameters.AddWithValue("@sname",student.Name);
            cmd.Parameters.AddWithValue("@branch",student.Branch);
            cmd.Parameters.AddWithValue("@percentage",student.Percentage);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

       public int UpdateStudent(Student student)
        {
            string qry = "update Student set Name=@name,Branch=@branch,Percentage=@percentage where RollNo=@roll";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@roll", student.RollNo);
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@branch", student.Branch);
            cmd.Parameters.AddWithValue("@percentage", student.Percentage);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public Student GetStudentByRoll(int roll)
        {
            Student student = new Student();
            string qry  = "select * from Student where RollNo=@roll";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@roll", roll);
            con.Open();
            dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    student.RollNo = Convert.ToInt32(dr["RollNo"]);
                    student.Name = dr["Name"].ToString();
                    student.Branch = dr["Branch"].ToString();
                    student.Percentage = Convert.ToInt32(dr["Percentage"]);
                }
            }

            con.Close();
            return student;
        }

        public int DeleteStudent(Student student)
        {
            string qry = "delete from Student where RollNo=@roll";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@roll", student.RollNo);
            
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
      public DataTable GetAllStudent()
        {
            DataTable table = new DataTable();
            string qry = "selet * from Student";
            cmd = new SqlCommand(qry, con);
            con.Open();dr = cmd.ExecuteReader();
            table.Load(dr);
            con.Close();
            
                           
            return table;
        }
      
    }
}
