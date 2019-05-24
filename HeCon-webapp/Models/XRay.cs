using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HeCon_webapp.Models
{
    public class XRay
    {
        [Key]
        public int ImageId { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        [DisplayName("Upload File")]
        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public Prediction Prediction { get; set; }

        public int PermissionToDoctor {get;set;}
    }
}