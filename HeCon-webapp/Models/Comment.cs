using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HeCon_webapp.Models
{
    public class Comment
    {
        public Comment()
        {
            AddedDate = DateTime.Now;
            LastEdit = DateTime.Now;
        }
        [Key]
        public int CommentId { get; set; }

        public string UserId { get; set; } 
        [Required]
        [MaxLength(255)]
        public String Text { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime LastEdit { get; set; }


        public int ImageId { get; set; }

        public XRay XRays { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}