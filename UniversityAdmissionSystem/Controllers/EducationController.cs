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
    public class EducationController : ApiController
    {
        //API METHOD TO ADD THE ACADEMIC DETAILS
        [HttpPost]
        public void Add(Education education)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into tbeducation values(@eappid,@yop10,@board10@percent10,@yop12,@board12,@percent12,@yopgrad,@univgrad,@percentgrad)", conn);
            cmd.Parameters.Add(new SqlParameter("@eappid",education.eappid));
            cmd.Parameters.Add(new SqlParameter("@yop10", education.yop_10));
            cmd.Parameters.Add(new SqlParameter("@board10", education.board_10));
            cmd.Parameters.Add(new SqlParameter("@percent10", education.percent_10));
            cmd.Parameters.Add(new SqlParameter("@yop12", education.yop_12));
            cmd.Parameters.Add(new SqlParameter("@board12", education.board_12));
            cmd.Parameters.Add(new SqlParameter("@percent12", education.percent_12));
            cmd.Parameters.Add(new SqlParameter("@yopgrad", education.yop_grad));
            cmd.Parameters.Add(new SqlParameter("@univgrad", education.univ_grad));
            cmd.Parameters.Add(new SqlParameter("@percentgrad", education.percent_grad));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }

        //API METHOD TO UPDATE THE ACADEMIC DETAILS
        [HttpPost]
        public void Update(Education education)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("update tbeducation set eappid=@eappid, yop_10=@yop10, board_10=@board10, percent_10=@percent10, yop_12=@yop12, board_12=@board12, percent_12=@percent12, yop_grad=@yopgrad, univ_grad=@univgrad, percent_grad=@percentgrad where eid=@eid", conn);
            cmd.Parameters.Add(new SqlParameter("@eid",education.eid));
            cmd.Parameters.Add(new SqlParameter("@eappid", education.eappid));
            cmd.Parameters.Add(new SqlParameter("@yop10", education.yop_10));
            cmd.Parameters.Add(new SqlParameter("@board10", education.board_10));
            cmd.Parameters.Add(new SqlParameter("@percent10", education.percent_10));
            cmd.Parameters.Add(new SqlParameter("@yop12", education.yop_12));
            cmd.Parameters.Add(new SqlParameter("@board12", education.board_12));
            cmd.Parameters.Add(new SqlParameter("@perecnt12", education.percent_12));
            cmd.Parameters.Add(new SqlParameter("@yopgrad", education.yop_grad));
            cmd.Parameters.Add(new SqlParameter("@univgrad", education.univ_grad));
            cmd.Parameters.Add(new SqlParameter("@percentgrad", education.percent_grad));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
    }
}
