using EvaluationFormsManager.Domain;
using System.Collections.Generic;

namespace EvaluationFormsManager.Shared
{
    public interface IFormService
    {
        Form GetForm(int formIdentifier);
        IEnumerable<Form> GetAllForms();

        void AddForm(Form form);
        void DeleteForm(Form form);
        void UpdateForm(Form form);

        Section GetSection(int sectionIdentifier);

        IEnumerable<Status> GetAllStatuses();
        IEnumerable<Importance> GetAllImportances();
    }
}
