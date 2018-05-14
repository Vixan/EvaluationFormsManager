using EvaluationFormsManager.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EvaluationFormsManager.Models
{
    public class FormCreateVM
    {
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please provide the form name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Display(Name = "Importance")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please specify the form importance")]
        public int ImportanceId { get; set; }

        public IEnumerable<Importance> ImportanceList { get; set; }
        public ICollection<string> ImportanceColors { get; set; }

        [Display(Name = "Status")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please provide the form status")]
        public int StatusId { get; set; }

        public IEnumerable<Status> StatusList { get; set; }
        public ICollection<string> StatusColors { get; set; }

        [Display(Name = "Sections")]
        public IEnumerable<Section> Sections { get; set; }
    }
}
