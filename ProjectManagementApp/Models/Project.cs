using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManagementApp.Models
{
    public class Project
    {
        //public int Id { get; set; }
        
        [Display(Name = "Project Name")]
        [Required(ErrorMessage = "Please enter a project name.")]
        public string ProjectName { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}