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
    public class StreamController : ApiController
    {
        // API method to ADD the stream
        [HttpPost]
        public void Add(Stream stream)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into tbstream values(@sname)", conn);
            cmd.Parameters.Add(new SqlParameter("@sname", stream.sname));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }

        // API method to UPDATE the stream
        [HttpPost]
        public void Update(Stream stream)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("update tbstream set sname=@sname where sid=@sid", conn);
            cmd.Parameters.Add(new SqlParameter("@sid", stream.sid));
            cmd.Parameters.Add(new SqlParameter("@sname", stream.sname));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }

        // API method to FETCH a particular stream
        [HttpGet]
        public List<Stream> Get(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * from tbstream where sid=@sid", conn);
            cmd.Parameters.Add(new SqlParameter("@sid", id));
            SqlDataReader reader = cmd.ExecuteReader();
            List<Stream> lst = new List<Stream>();
            Stream stream = new Stream();
            while (reader.Read())
            {
                stream.sid = int.Parse(reader["sid"].ToString());
                stream.sname = reader["sname"].ToString();
                lst.Add(stream);
            }
            return lst;
        }

        // API METHOD TO FETCH ALL STREAMS
        [HttpGet]
        public List<Stream> GetAll()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from tbstream", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Stream> lst = new List<Stream>();
           
            while (reader.Read())
            {
                Stream stream = new Stream();
                stream.sid = int.Parse(reader["sid"].ToString());
                stream.sname = reader["sname"].ToString();
                lst.Add(stream);
            }
            return lst;
        }

        //API METHOD TO DELETE A STREAM
        [HttpGet]
        public bool Delete(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete from tbstream where sid=@sid", conn);
            cmd.Parameters.Add(new SqlParameter("@sid", id));
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
