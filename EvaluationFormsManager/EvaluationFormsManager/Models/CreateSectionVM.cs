using EvaluationFormsManager.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EvaluationFormsManager.Models
{
    public class CreateSectionVM
    {
        public int UserId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Criteria")]
        public ICollection<Criteria> Criteria { get; set; }

        [Display(Name = "Importance")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please specify the form importance")]
        public int ImportanceId { get; set; }

        public IEnumerable<SelectListItem> ImportanceList { get; set; }


        public Criteria CreateCriteria()
        {
            Criteria newCriteria = new Criteria()
            {
                Name = "Enter name here",
                CreatedBy = UserId,
                ModifiedBy = UserId,
                ModifiedDate = DateTime.Now
            };

            Criteria.Add(newCriteria);

            return newCriteria;
        }
    }
}
