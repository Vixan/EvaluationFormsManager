using EvaluationFormsManager.Domain;
using EvaluationFormsManager.Persistence;
using EvaluationFormsManager.Shared;
using System;
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
            formRepository.Save();
        }

        public void DeleteForm(Form form)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            formRepository.Delete(form);
            formRepository.Save();
        }

        public IEnumerable<Form> GetAllForms()
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            return formRepository.GetAll();
        }

        public IEnumerable<Form> GetOwnedForms(string userIdentifier)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            return formRepository.GetOwned(userIdentifier);
        }

        public IEnumerable<Form> GetSharedForms(string userIdentifier)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            return formRepository.GetShared(userIdentifier);
        }

        public IEnumerable<Importance> GetAllImportances()
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();
            IEnumerable<Importance> importances = formRepository.GetImportances();

            return importances;
        }

        public IEnumerable<Status> GetAllStatuses()
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();
            IEnumerable<Status> statuses = formRepository.GetStatuses();

            return statuses;
        }

        public Form GetForm(int formIdentifier)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            return formRepository.GetById(formIdentifier);
        }

        public Section GetSection(int sectionIdentifier)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            return formRepository.GetSection(sectionIdentifier);
        }

        public void UpdateForm(Form form)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();
            IEnumerable<Form> forms = formRepository.GetAll();

            Form formToUpdate = forms.Where(searchedForm => searchedForm.Id == form.Id).FirstOrDefault();

            formToUpdate.Name = form.Name;
            formToUpdate.Description = form.Description;
            formToUpdate.Importance = form.Importance;
            formToUpdate.Status = form.Status;
            formToUpdate.ModifiedDate = form.ModifiedDate;
            formToUpdate.ModifiedBy = form.ModifiedBy;

            formRepository.Save();
        }

        public void ShareForm(Form form, IEnumerable<string> shareWithUsers)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            formRepository.Share(form, shareWithUsers);

            formRepository.Save();
        }

        public void UnshareForm(Form form, IEnumerable<string> unshareWithUsers)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            formRepository.Unshare(form, unshareWithUsers);

            formRepository.Save();
        }
    }
}
