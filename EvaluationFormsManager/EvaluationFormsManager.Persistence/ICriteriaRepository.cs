using EvaluationFormsManager.Domain;
using System;
using System.Collections.Generic;

namespace EvaluationFormsManager.Persistence
{
    public interface ICriteriaRepository : IRepository<Criteria>
    {
        Criteria GetByName(string criteriaName);

        IEnumerable<Criteria> GetByCreatedDate(DateTime createdDate);
        IEnumerable<Criteria> GetByModifiedDate(DateTime modifiedDate);

        IEnumerable<Criteria> GetCreatedBy(int userIdentifier);
        IEnumerable<Criteria> GetModifiedBy(int userIdentifier);

        IEnumerable<Criteria> GetByEvaluationScaleOption(int evaluationScaleOptionIdentifier);
    }
}
