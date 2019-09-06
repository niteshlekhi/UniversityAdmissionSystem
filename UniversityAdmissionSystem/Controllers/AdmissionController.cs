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
    public class AdmissionController : ApiController
    {
        //API METHOD TO ADD THE NEW ADMISSION
        [HttpPost]
        public void Add(Admission admission)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UASDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into tbadmisssion values(@aappid,@asid,@adid,@acid,@sts)", conn);
            cmd.Parameters.Add(new SqlParameter("@aappid", admission.aappid));
            cmd.Parameters.Add(new SqlParameter("@asid", admission.asid));
            cmd.Parameters.Add(new SqlParameter("@adid",admission.adid));
            cmd.Parameters.Add(new SqlParameter("@acid", admission.acid));
            cmd.Parameters.Add(new SqlParameter("@asts", admission.asts));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }

        
            }
}
