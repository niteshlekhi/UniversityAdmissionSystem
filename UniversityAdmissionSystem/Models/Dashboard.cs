using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityAdmissionSystem.Models
{
    public class Dashboard
    {
        public int TotalApplicants { get; set; }
        public int TotalApproved { get; set; }
        public int TotalRejected { get; set; }
        public int TotalPending { get; set; }
    }
}