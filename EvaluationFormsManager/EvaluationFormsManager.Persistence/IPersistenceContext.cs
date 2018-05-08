using EvaluationFormsManager.CommonAbstractions;

namespace EvaluationFormsManager.Persistence
{
    public interface IPersistenceContext : IInitializer
    {
        IFormRepository GetEvaluationRepository();
        ISectionRepository GetSectionRepository();
        ICriteriaRepository GetCriteriaRepository();
    }
}
