using EvaluationFormsManager.CommonAbstractions;

namespace EvaluationFormsManager.Persistence
{
    public interface IPersistenceContext : IInitializer
    {
        IFormRepository GetFormRepository();
        ISectionRepository GetSectionRepository();
        ICriteriaRepository GetCriteriaRepository();
    }
}
