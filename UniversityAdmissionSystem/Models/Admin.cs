using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityAdmissionSystem.Models
{
    public class Admin
    {
        public int admid { get; set; }
        public string admname { get; set; }
        public string admemail { get; set; }
        public string admpwd { get; set; }

    }
}