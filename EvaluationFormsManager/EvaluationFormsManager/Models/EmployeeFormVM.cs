using Microsoft.ApplicationInsights.AspNetCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace EvaluationFormsManager.Models
{
    public class EmployeeFormVM
    {
        [Display(Name = "Identifier")]
        [DataType(DataType.Text)]
        public int Id { get; set; }

        [Display(Name = "Form Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Display(Name = "Importance Level")]
        [DataType(DataType.Text)]
        public int ImportanceLevel { get; set; }

        [Display(Name = "Status")]
        [DataType(DataType.Text)]
        public bool Status { get; set; }

        [Display(Name = "Created")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd.MMM HH:mm tt}")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Modified")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd.MMM HH:mm tt}")]
        public DateTime ModifiedDate { get; set; }
    }
}
