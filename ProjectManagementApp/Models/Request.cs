using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ProjectManagementApp.Models
{
    public class Request
    {
        //public int Id { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = "Please enter a Subject.")]
        public string Subject { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter details of your request here.")]
        public string Description { get; set; }

        //[Required(ErrorMessage = "Please enter a status.")]
        //[Display(Name = "Status")]
        //public string SelectedStatus { get; set; }
        //public IEnumerable<SelectListItem> Statuses { get; set; }
    }
}