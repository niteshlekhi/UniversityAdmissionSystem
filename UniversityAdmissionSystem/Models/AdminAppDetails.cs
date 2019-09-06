using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityAdmissionSystem.Models
{
    public class AdminAppDetails
    {
        public int aid { get; set; }
        public string appfname { get; set; }
        public string appfcontact { get; set; }
        public string appname { get; set; }
        public string appemail { get; set; }
        public string appcontact { get; set; }
        public string appaddress { get; set; }
        public string appgender { get; set; }
        public string appDOB { get; set; }
        public string sname { get; set; }
        public string dname { get; set; }
        public string cname { get; set; }
        public int asts { get; set; }
        public int appid { get; set; }

    }

    public class AdminAppFullDetails
    {
        public string appname { get; set; }
        public string appemail { get; set; }
        public string appcontact { get; set; }
        public string appfname { get; set; }
        public string appfcontact { get; set; }
        public string appaddress { get; set; }
        public string appgender { get; set; }
        public string appDOB { get; set; }
        public int yop_10 { get; set; }
        public string board_10 { get; set; }
        public int percent_10 { get; set; }
        public int yop_12 { get; set; }
        public string board_12 { get; set; }
        public int percent_12 { get; set; }
        public int yop_grad { get; set; }
        public string univ_grad { get; set; }
        public int percent_grad { get; set; }

    }
}