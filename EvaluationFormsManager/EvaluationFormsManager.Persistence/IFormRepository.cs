using EvaluationFormsManager.Domain;
using System;
using System.Collections.Generic;

namespace EvaluationFormsManager.Persistence
{
    public interface IFormRepository : IRepository<Form>
    {
        Form GetByName(string evaluationName);

        IEnumerable<Form> GetAVailable();
        IEnumerable<Form> GetUnavailable();

        IEnumerable<Form> GetByCreatedDate(DateTime createdDate);
        IEnumerable<Form> GetByModifiedDate(DateTime modifiedDate);

        IEnumerable<Form> GetCompleted();
        IEnumerable<Form> GetUncompleted();

        IEnumerable<Form> GetCreatedBy(int userIdentifier);
        IEnumerable<Form> GetModifiedBy(int userIdentifier);
    }
}
