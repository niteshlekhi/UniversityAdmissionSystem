using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityAdmissionSystem.Models
{
    public class Education
    {
        public int eid { get; set; }
        public int eappid { get; set; }
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