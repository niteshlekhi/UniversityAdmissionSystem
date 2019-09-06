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
    public class AdminController : ApiController
    {
        // API method for get all applications for Admin
        public List<AdminAppDetails> GetAllApplicants()
        {
            SqlDataReader reader = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT aid, appfname,appfcontact,appname,appemail,appcontact,appaddress,appgender,appDOB,sname,dname,cname,asts,appid from tbadmission INNER JOIN tbapplicant on tbadmission.aappid=tbapplicant.appid inner join tbstream on tbadmission.asid=tbstream.sid INNER JOIN  tbdepartment on tbadmission.adid=tbdepartment.did inner join  tbcourse on tbadmission.acid=tbcourse.cid", con);
            con.Open();
            reader = cmd.ExecuteReader();
            AdminAppDetails app = null;
            List<AdminAppDetails> lstapp = new List<AdminAppDetails>();
            while (reader.Read())
            {

                app = new AdminAppDetails();
                app.aid = int.Parse(reader[0].ToString());
                app.appfname = reader[1].ToString();
                app.appfcontact = reader[2].ToString();
                app.appname = reader[3].ToString();
                app.appemail = reader[4].ToString();
                app.appcontact = reader[5].ToString();
                app.appaddress = reader[6].ToString();
                app.appgender = reader[7].ToString();
                app.appDOB = reader[8].ToString();
                app.sname = reader[9].ToString();
                app.dname = reader[10].ToString();
                app.cname = reader[11].ToString();
                app.asts = int.Parse(reader[12].ToString());
                app.appid=int.Parse(reader[13].ToString());
                lstapp.Add(app);
            }
            return lstapp;

        }

        // API method for get Ful details of an applicant
        public List<AdminAppFullDetails> GetFullDetails(int Id)
        {
            SqlDataReader reader = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT appid, appname, appemail, appcontact, appfname, appfcontact, appaddress,appgender,appDOB, yop_10, board_10, percent_10, yop_12, board_12, percent_12, yop_grad, univ_grad, percent_grad  from tbapplicant" +
                " LEFT JOIN tbeducation on tbapplicant.appid=tbeducation.eappid WHERE appid=@id", con);
            cmd.Parameters.Add(new SqlParameter("@id", Id));
            con.Open();
            reader = cmd.ExecuteReader();
            AdminAppFullDetails app = null;
            List<AdminAppFullDetails> lstapp = new List<AdminAppFullDetails>();
            while (reader.Read())
            {
                app = new AdminAppFullDetails();
                app.appname = reader["appname"].ToString();
                app.appemail = reader["appemail"].ToString();
                app.appcontact = reader["appcontact"].ToString();
                app.appfname = reader["appfname"].ToString();
                app.appfcontact = reader["appfcontact"].ToString();
                app.appaddress = reader["appaddress"].ToString();
                app.appgender = reader["appgender"].ToString();
                app.appDOB = reader["appDOB"].ToString();
                app.yop_10 = Convert.ToInt32(reader["yop_10"].ToString());
                app.board_10 = reader["board_10"].ToString();
                app.percent_10 = Convert.ToInt32(reader["percent_10"].ToString());
                app.yop_12 = Convert.ToInt32(reader["yop_12"].ToString());
                app.board_12 = reader["board_12"].ToString();
                app.percent_12 = Convert.ToInt32(reader["percent_12"].ToString());
                app.yop_grad = Convert.ToInt32(reader["yop_grad"].ToString());
                app.univ_grad = reader["univ_grad"].ToString();
                app.percent_grad = Convert.ToInt32(reader["percent_grad"].ToString());
                lstapp.Add(app);
            }
            return lstapp;

        }

        // API method to Update Application Status 
        [HttpPost]
        public int UpdateStatus(Admission admission)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE tbadmission SET asts=@asts where aid=@aid", conn);
            cmd.Parameters.Add(new SqlParameter("@asts", admission.asts));
            cmd.Parameters.Add(new SqlParameter("@aid", admission.aid));
            int result = cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            return result;
        }

        [HttpGet]
        public Dashboard GetDashboard()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
           
            Dashboard dash = new Dashboard();
            con.Open();
            //-------------------TOTAL APPLICANTS----------------------------
            SqlCommand cmd = new SqlCommand("select count(*) from tbapplicant", con);
            dash.TotalApplicants = Convert.ToInt32(cmd.ExecuteScalar());

            //-------------------TOTAL APPROVED------------------------------
            cmd = new SqlCommand("select count(*)  from tbadmission inner join tbapplicant on tbadmission.aappid=tbapplicant.appid inner join tbstream on tbadmission.asid=tbstream.sid inner join  tbdepartment on tbadmission.adid=tbdepartment.did inner join  tbcourse on tbadmission.acid=tbcourse.cid where asts=1", con);
            dash.TotalApproved = Convert.ToInt32(cmd.ExecuteScalar());

            //-------------------TOTAL REJECTED------------------------------
            cmd = new SqlCommand("select count(*)  from tbadmission inner join tbapplicant on tbadmission.aappid=tbapplicant.appid inner join tbstream on tbadmission.asid=tbstream.sid inner join  tbdepartment on tbadmission.adid=tbdepartment.did inner join  tbcourse on tbadmission.acid=tbcourse.cid where asts=-1", con);
            dash.TotalRejected = Convert.ToInt32(cmd.ExecuteScalar());

            //-------------------TOTAL PENDING------------------------------
            cmd = new SqlCommand("select count(*)  from tbadmission inner join tbapplicant on tbadmission.aappid=tbapplicant.appid inner join tbstream on tbadmission.asid=tbstream.sid inner join  tbdepartment on tbadmission.adid=tbdepartment.did inner join  tbcourse on tbadmission.acid=tbcourse.cid where asts=0", con);
            dash.TotalPending = Convert.ToInt32(cmd.ExecuteScalar());
            
            return dash;
        }
    }
}
