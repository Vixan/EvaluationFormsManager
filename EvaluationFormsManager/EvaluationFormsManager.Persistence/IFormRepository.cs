using EvaluationFormsManager.Domain;
using System;
using System.Collections.Generic;

namespace EvaluationFormsManager.Persistence
{
    public interface IFormRepository : IRepository<Form>
    {
        Form GetByName(string formName);

        Section GetSection(int sectionIdentifier);

        IEnumerable<Importance> GetImportances();
        IEnumerable<Status> GetStatuses();
        IEnumerable<Section> GetSections();
    }
}
