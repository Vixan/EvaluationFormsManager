using EvaluationFormsManager.Domain;
using System.Collections.Generic;

namespace EvaluationFormsManager.Shared
{
    public interface IFormService
    {
        Form GetForm(int formIdentifier);
        IEnumerable<Form> GetAllForms();
        IEnumerable<Form> GetOwnedForms(string userIdentifier);
        IEnumerable<Form> GetSharedForms(string userIdentifier);

        void AddForm(Form form);
        void DeleteForm(Form form);
        void UpdateForm(Form form);
        void ShareForm(Form form, string shareWithUserId);

        Section GetSection(int sectionIdentifier);

        IEnumerable<Status> GetAllStatuses();
        IEnumerable<Importance> GetAllImportances();

    }
}
