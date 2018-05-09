using EvaluationFormsManager.Domain;
using System;
using System.Collections.Generic;

namespace EvaluationFormsManager.Persistence
{
    public interface IFormRepository : IRepository<Form>
    {
        Form GetByName(string formName);

        IEnumerable<Form> GetAvailable();
        IEnumerable<Form> GetUnavailable();

        IEnumerable<Form> GetByCreatedDate(DateTime createdDate);
        IEnumerable<Form> GetByModifiedDate(DateTime modifiedDate);

        IEnumerable<Form> GetCreatedBy(int employeeIdentifier);
        IEnumerable<Form> GetModifiedBy(int employeeIdentifier);

        IEnumerable<Section> GetSectionsByEvaluationScale(int formIdentifier, int evaluationScaleIdentifier);
        Section GetSection(int formIdentifier, int sectionIdentifier);
        IEnumerable<Criteria> GetSectionCriteria(int formIdentifier, int sectionIdentifier);
        Criteria GetSectionCriterion(int formIdentifier, int sectionIdentifier, int criterionIdentifier);
    }
}
