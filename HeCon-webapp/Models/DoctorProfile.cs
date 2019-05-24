using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HeCon_webapp.Models
{
    public class DoctorProfile
    {
        [Key]
        public string UserId { get; set; }
        //public int DoctorProfileId { get; set; }

        [Required(ErrorMessage = "Numele doctorului este obligatoriu")]
        public string DoctorName { get; set; }

        public string Description { get; set; }


        public virtual ApplicationUser User { get; set; }
    }
}