using EvaluationFormsManager.CommonAbstractions;

namespace EvaluationFormsManager.Persistence
{
    public interface IPersistenceContext : IInitializer
    {
        IEvaluationRepository GetEvaluationRepository();
        ISectionRepository GetSectionRepository();
        ICriteriaRepository GetCriteriaRepository();
    }
}
