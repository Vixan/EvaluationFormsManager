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
        }

        public void DeleteForm(Form form)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            formRepository.Delete(form);
        }

        public void DeleteFormSection(int formIdentifier, Section section)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();
            Form form = formRepository.GetById(formIdentifier);

            form.Sections.Remove(section);
        }

        public IEnumerable<Form> GetAllAVailableForms()
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            return formRepository.GetAvailable();
        }

        public IEnumerable<Form> GetAllForms()
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            return formRepository.GetAll();
        }

        public IEnumerable<Form> GetAllFormsByCreatedDate(DateTime createdDate)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            return formRepository.GetByCreatedDate(createdDate);
        }

        public IEnumerable<Form> GetAllFormsByModifiedDate(DateTime modifiedDate)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            return formRepository.GetByModifiedDate(modifiedDate);
        }

        public IEnumerable<Form> GetAllFormsCreatedBy(int employeeIdentifier)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            return formRepository.GetCreatedBy(employeeIdentifier);
        }

        public IEnumerable<Criteria> GetAllFormSectionCriteria(int formIdentifier, int sectionIdentifier)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            return formRepository.GetSectionCriteria(formIdentifier, sectionIdentifier);
        }

        public IEnumerable<Section> GetAllFormSectionsByEvaluationScale(int formIdentifier, int evaluationScaleIdentifier)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            return formRepository.GetSectionsByEvaluationScale(formIdentifier, evaluationScaleIdentifier);
        }

        public IEnumerable<Form> GetAllFormsModifiedBy(int employeeIdentifier)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            return formRepository.GetModifiedBy(employeeIdentifier);
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

        public IEnumerable<Form> GetAllUnavailableForms()
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            return formRepository.GetUnavailable();
        }

        public Form GetForm(int formIdentifier)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            return formRepository.GetById(formIdentifier);
        }

        public Section GetFormSection(int formIdentifier, int sectionIdentifier)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            return formRepository.GetSection(formIdentifier, sectionIdentifier);
        }

        public Criteria GetFormSectionCriterion(int formIdentifier, int sectionIdentifier, int criterionIdentifier)
        {
            IFormRepository formRepository = persistanceContext.GetFormRepository();

            return formRepository.GetSectionCriterion(formIdentifier, sectionIdentifier, criterionIdentifier);
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
    }
}
