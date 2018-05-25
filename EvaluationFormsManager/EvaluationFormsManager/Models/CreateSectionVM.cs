using EvaluationFormsManager.Domain;
using System.ComponentModel.DataAnnotations;

namespace EvaluationFormsManager.Models
{
    public class CreateSectionVM
    {
        public string UserId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Evaluation Scale")]
        //[DataType(DataType.Text)]
        [Required(ErrorMessage = "Please specify the form evaluation scale")]
        public EvaluationScale EvaluationScale { get; set; }
    }
}
