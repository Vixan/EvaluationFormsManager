using EvaluationFormsManager.Domain;
using EvaluationFormsManager.Persistence;
using EvaluationFormsManager.Shared;
using System.Collections.Generic;
using System.Linq;

namespace EvaluationFormsManager.Core
{
    public class FormService : IFormService
    {
        private readonly IPersistenceContext persistanceContext;

        public FormService(IPersistenceContext persistanceContext)
        {
            this.persistanceContext = persistanceContext;
        }

        public void AddForm(Form form)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            formRepository.Add(form);
        }

        public void DeleteForm(Form form)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            formRepository.Delete(form);
        }

        public IEnumerable<Form> GetAllForms()
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            return formRepository.GetAll();
        }

        public Form GetForm(int formIdentifier)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            return formRepository.GetById(formIdentifier);
        }

        public void UpdateForm(Form form)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();
            IEnumerable<Form> forms = formRepository.GetAll();

            Form formToUpdate = forms.Where(searchedForm => searchedForm.Id == form.Id).FirstOrDefault();
            formToUpdate = form;
        }
    }
}
