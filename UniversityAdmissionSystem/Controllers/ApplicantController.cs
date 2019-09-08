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
    public class ApplicantController : ApiController
    {
        //  API METHOD TO Register THE APPLICANT
        [HttpPost]
        public int Register(Applicant applicant)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into tbapplicant values(@appfname,@appfcontact,@appname,@appemail,@apppwd,@appcontact,@appaddress,@appgender,@appDOB)", conn);
            cmd.Parameters.Add(new SqlParameter("@appfname", applicant.appfname));
            cmd.Parameters.Add(new SqlParameter("@appfcontact", applicant.appfcontact));
            cmd.Parameters.Add(new SqlParameter("@appname", applicant.appname));
            cmd.Parameters.Add(new SqlParameter("@appemail", applicant.appemail));
            cmd.Parameters.Add(new SqlParameter("@apppwd", applicant.apppwd));
            cmd.Parameters.Add(new SqlParameter("@appcontact", applicant.appcontact));
            cmd.Parameters.Add(new SqlParameter("@appaddress", applicant.appaddress));
            cmd.Parameters.Add(new SqlParameter("@appgender", applicant.appgender));
            cmd.Parameters.Add(new SqlParameter("@appDOB", applicant.appDOB));
           int result= cmd.ExecuteNonQuery();
            cmd.Dispose();
           

            SqlCommand cmd2 = new SqlCommand("select appid from tbapplicant where appemail=@appemail", conn);
            cmd2.Parameters.Add(new SqlParameter("@appemail", applicant.appemail));
            applicant.appid = Convert.ToInt32(cmd2.ExecuteScalar());
            AddEmptyEducation(applicant.appid);
            return result;
        }

        public void AddEmptyEducation(int Id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into tbeducation values(@eappid,0,NULL,0,0,NULL,0,0,NULL,0)", conn);
            cmd.Parameters.Add(new SqlParameter("@eappid", Id));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }

        // API method for get applied applications for particular Applicant
        public List<AdminAppDetails> GetAppliedApplications(int id)
        {
            SqlDataReader reader = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT aid, appfname,appfcontact,appname,appemail,appcontact,appaddress,appgender,appDOB,sname,dname,cname,asts,appid from tbadmission INNER JOIN tbapplicant on tbadmission.aappid=tbapplicant.appid inner join tbstream on tbadmission.asid=tbstream.sid INNER JOIN  tbdepartment on tbadmission.adid=tbdepartment.did inner join  tbcourse on tbadmission.acid=tbcourse.cid WHERE appid="+id, con);
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
                app.appid = int.Parse(reader[13].ToString());
                lstapp.Add(app);
            }
            return lstapp;

        }


        //api method to get the profile of the students







        public List<Profile> GetProfile(int id)
        {
            SqlDataReader reader = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select appfname,appfcontact,appname,appemail,appcontact,appaddress,appgender,appDOB,yop_10,board_10,percent_10,yop_12,board_12,percent_12,yop_grad,univ_grad,percent_grad from tbapplicant left join tbeducation on tbapplicant.appid=tbeducation.eappid where tbapplicant.appid=@id", con);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            con.Open();
            reader = cmd.ExecuteReader();
            Profile app = null;
            List<Profile> lstapp = new List<Profile>();
            while (reader.Read())
            {
                app = new Profile();
                app.appfname = reader[0].ToString();
                app.appfcontact = reader[1].ToString();
                app.appname = reader[2].ToString();
                app.appemail = reader[3].ToString();
                app.appcontact = reader[4].ToString();
                app.appaddress = reader[5].ToString();
                app.appgender = reader[6].ToString();
                app.appDOB = Convert.ToDateTime(reader[7].ToString());

                app.yop_10 = int.Parse(reader[8].ToString());
                app.board_10 = reader[9].ToString();
                app.percent_10 = int.Parse(reader[10].ToString());

                app.yop_12 = int.Parse(reader[11].ToString());
                app.board_12 = reader[12].ToString();
                app.percent_12 = int.Parse(reader[13].ToString());

                app.yop_grad = int.Parse(reader[14].ToString());
                app.univ_grad = reader[15].ToString();
                app.percent_grad = int.Parse(reader[16].ToString());
                lstapp.Add(app);
            }
            return lstapp;

        }

    }
}
