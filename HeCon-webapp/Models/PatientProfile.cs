using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HeCon_webapp.Models
{
    public class PatientProfile
    {
        [Key]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Numele pacientului este obligatoriu")]
        public string PatientName { get; set; }

        public int Age { get; set; }

        public string HealthIssues { get; set; }

        public string SurgicalProcedures { get; set; }

        public string FamilyDoctor { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}