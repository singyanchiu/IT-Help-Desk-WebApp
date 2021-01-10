using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLibraryNew2.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string Subject { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter details of your request here.")]
        public string Description { get; set; }

        [Display(Name = "Status")]
        public int SelectedStatus { get; set; }
        public string Status { get; set; }
        public List<Status> Statuses { get; set; }
        public string Submitter { get; set; }
    }
}
