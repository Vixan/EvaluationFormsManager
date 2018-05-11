using EvaluationFormsManager.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationFormsManager.Models
{
    public class FormEditVM
    {
        [Display(Name = "Identifier")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please provide the form identifier")]
        public int Id { get; set; }

        [Display(Name = "Form Name")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please provide the form name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Display(Name = "Importance Level")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please specify the form importance")]
        public int ImportanceLevel { get; set; }

        [Display(Name = "Status")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please provide the form status")]
        public Status Status { get; set; }

        [Display(Name = "Sections")]
        public IEnumerable<Section> Sections { get; set; }
    }
}
