using EvaluationFormsManager.Domain;
using System;
using System.Collections.Generic;

namespace EvaluationFormsManager.Persistence
{
    public interface IEvaluationRepository : IRepository<Evaluation>
    {
        Evaluation GetByName(string evaluationName);

        IEnumerable<Evaluation> GetAVailable();
        IEnumerable<Evaluation> GetUnavailable();

        IEnumerable<Evaluation> GetByCreatedDate(DateTime createdDate);
        IEnumerable<Evaluation> GetByModifiedDate(DateTime modifiedDate);

        IEnumerable<Evaluation> GetCompleted();
        IEnumerable<Evaluation> GetUncompleted();

        IEnumerable<Evaluation> GetCreatedBy(int userIdentifier);
        IEnumerable<Evaluation> GetModifiedBy(int userIdentifier);
    }
}
