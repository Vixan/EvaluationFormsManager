using System.ComponentModel.DataAnnotations;

namespace EvaluationFormsManager.DataTransferObjects
{
    public enum EvaluationScaleDTO
    {
        [Display(Name = "Agreement")]
        Agreement = 1,
        [Display(Name = "Satisfaction")]
        Satisfaction,
        [Display(Name = "Skill Level")]
        SkillLevel,
        [Display(Name = "Points")]
        Points,
        [Display(Name = "Grades")]
        Grades
    }
}
