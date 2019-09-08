
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
    public class CourseController : ApiController
    {
        // POST: METHOD TO ADD A COURSE
        [HttpPost]
        public void Add(Course course)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into tbcourse values(@cname,@cdid)", conn);
            cmd.Parameters.Add(new SqlParameter("@cname", course.cname));
            cmd.Parameters.Add(new SqlParameter("@cdid", course.cdid));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();

        }

        // POST:  METHOD TO UPDATE THE COURSE
        [HttpPost]
        public void Update(Course course)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("update tbcourse set cname=@cname, cdid=@cdid where cid=@cid", conn);
            cmd.Parameters.Add(new SqlParameter("@cid", course.cid));
            cmd.Parameters.Add(new SqlParameter("@cname", course.cname));
            cmd.Parameters.Add(new SqlParameter("@cdid", course.cdid));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }

        // GET: METHOD TO FETCH A PARTICULAR COURSE
        [HttpGet]
        public List<Course> Get(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from tbcourse where cid=@cid", conn);
            cmd.Parameters.Add(new SqlParameter("@cid", id));
            SqlDataReader reader = cmd.ExecuteReader();
            List<Course> lst = new List<Course>();
            while (reader.Read())
            {
                Course course = new Course();
                course.cid = int.Parse(reader["cid"].ToString());
                course.cname = reader["cname"].ToString();
                course.cdid = int.Parse(reader["cdid"].ToString());
                lst.Add(course);
            }
            return lst;
        }

        // GET: METHOD TO FETCH ALL COURSES
        [HttpGet]
        public List<Course> GetAll()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(" select cid, cname, dname from tbcourse INNER JOIN tbdepartment on tbcourse.cdid = tbdepartment.did", conn);

            SqlDataReader reader = cmd.ExecuteReader();
            List<Course> lst = new List<Course>();
            while (reader.Read())
            {
                Course course = new Course();
                course.cid = int.Parse(reader["cid"].ToString());
                course.cname = reader["cname"].ToString();
                course.dname = reader["dname"].ToString();
                lst.Add(course);
            }
            return lst;
        }

        // GET: METHOD TO DELETE A COURSE
        [HttpGet]
        public bool Delete(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete from tbcourse where cid=@cid", conn);
            cmd.Parameters.Add(new SqlParameter("@cid", id));
            int n = cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            if (n > 1)
                return true;
            else
                return false;
        }

        [Route("api/Course/GetCourseFromDept/{deptId}")]
        [HttpGet]
        public List<Course> GetCourseFromDept(int deptId)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from tbcourse where cdid=@did", conn);
            cmd.Parameters.Add(new SqlParameter("@did", deptId));
            SqlDataReader reader = cmd.ExecuteReader();
            List<Course> lst = new List<Course>();
            while (reader.Read())
            {
                Course course = new Course();
                course.cid = int.Parse(reader["cid"].ToString());
                course.cname = reader["cname"].ToString();
                course.cdid = int.Parse(reader["cdid"].ToString());
                lst.Add(course);
            }
            return lst;
        }
    }
}
