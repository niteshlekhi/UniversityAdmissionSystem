using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UniversityAdmissionSystem.Models;

namespace UniversityAdmissionSystem.Controllers
{
    public class DepartmentController : ApiController
    {
        // API METHOD TO ADD THE DEPARTMENT
        [HttpPost]
        public void Add(Department dept)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into tbdepartment values(@dname,@dsid)", conn);
            cmd.Parameters.Add(new SqlParameter("@dname", dept.dname));
            cmd.Parameters.Add(new SqlParameter("@dsid", dept.dsid));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }

        // API METHOD TO UPDATE THE DEPARTMENT
        [HttpPost]
        public void Update(Department dept)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("update tbdepartment set dname=@dname, dsid=@dsid where did=@did", conn);
            cmd.Parameters.Add(new SqlParameter("@did", dept.did));
            cmd.Parameters.Add(new SqlParameter("@dname", dept.dname));
            cmd.Parameters.Add(new SqlParameter("@dsid", dept.dsid));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }


        //API METHOD TO FETCH A PARTICULAR DEPARTMENT
        [HttpGet]
        public List<Department> Get(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from tbdepartment where did=@did", conn);
            cmd.Parameters.Add(new SqlParameter("@did", id));
            SqlDataReader reader = cmd.ExecuteReader();
            List<Department> lst = new List<Department>();
            Department dept = new Department();
            while (reader.Read())
            {
                dept.did = int.Parse(reader["did"].ToString());
                dept.dname = reader["dname"].ToString();
                dept.dsid = int.Parse(reader["dsid"].ToString());
                lst.Add(dept);
            }
            return lst;
        }

        //API METHOD TO FETCH ALL DEPARTMENTS
        [HttpGet]
        public List<Department> GetAll()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from tbdepartment INNER JOIN tbstream on tbdepartment.dsid=tbstream.sid", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Department> lst = new List<Department>();
            
            while (reader.Read())
            {
                Department dept = new Department();
                dept.did = int.Parse(reader["did"].ToString());
                dept.dname = reader["dname"].ToString();
                dept.sname = reader["sname"].ToString();
                lst.Add(dept);
            }
            return lst;
        }

        //API METHOD TO DELETE A DEPARTMENT
        [HttpGet]
        public bool Delete(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete from tbdepartment where did=@did", conn);
            cmd.Parameters.Add(new SqlParameter("@did", id));
            int n = cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            if (n > 1)
                return true;
            else
                return false;
        }
    }
}
