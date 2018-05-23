using System.ComponentModel.DataAnnotations;

namespace EvaluationFormsManager.Domain
{
    public enum EvaluationScale
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
