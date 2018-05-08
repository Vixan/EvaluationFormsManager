using EvaluationFormsManager.Domain;
using System;
using System.Collections.Generic;

namespace EvaluationFormsManager.Persistence
{
    public interface ISectionRepository : IRepository<Section>
    {
        Section GetByName(string sectionName);

        IEnumerable<Section> GetByEvaluationScale(int evaluationScaleIdentifier);

        IEnumerable<Section> GetByCreatedDate(DateTime createdDate);
        IEnumerable<Section> GetByModifiedDate(DateTime modifiedDate);

        IEnumerable<Section> GetCreatedBy(int employeeIdentifier);
        IEnumerable<Section> GetModifiedBy(int employeeIdentifier);
    }
}
