using EvaluationFormsManager.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvaluationFormsManager.Persistence.EF
{
    class FormRepository : Repository<Form>, IFormRepository
    {
        public FormRepository(DatabaseContext context)
        {
            databaseContext = context;
        }

        public override IEnumerable<Form> GetAll()
        {
            IEnumerable<Form> forms = databaseContext.Forms
                .Include(form => form.Status)
                .Include(form => form.Importance)
                .Include(form => form.Sections);

            return forms;
        }

        public override Form GetById(int identifier)
        {
            Form foundForm = databaseContext.Forms
                .Where(form => form.Id == identifier)
                .Include(form => form.Status)
                .Include(form => form.Importance)
                .Include(form => form.Sections)
                .FirstOrDefault();

            return foundForm;
        }

        #region getByUser

        public IEnumerable<Form> GetCreatedBy(int employeeIdentifier)
        {
            IEnumerable<Form> formsCreatedBy = databaseContext.Forms
                .Where(form => form.CreatedBy == employeeIdentifier)
                .Include(form => form.Importance)
                .Include(form => form.Status)
                .ToList();

            return formsCreatedBy;
        }

        public IEnumerable<Form> GetModifiedBy(int employeeIdentifier)
        {
            IEnumerable<Form> formsModifiedBy = databaseContext.Forms.Where(form => form.ModifiedBy == employeeIdentifier).ToList();

            return formsModifiedBy;
        }

        #endregion

        #region getByData

        public Form GetByName(string formName)
        {
            Form formByName = databaseContext.Forms.Where(form => form.Name == formName).FirstOrDefault();

            return formByName;
        }

        public IEnumerable<Form> GetAvailable()
        {
            IEnumerable<Form> availableForms = databaseContext.Forms.Where(form => form.Status.Name == "Enabled").ToList();

            return availableForms;
        }

        public IEnumerable<Form> GetUnavailable()
        {
            IEnumerable<Form> unavailableForms = databaseContext.Forms.Where(form => form.Status.Name == "Disabled").ToList();

            return unavailableForms;
        }

        public IEnumerable<Form> GetByCreatedDate(DateTime createdDate)
        {
            IEnumerable<Form> formsByCreatedDate = databaseContext.Forms.Where(form => form.CreatedDate == createdDate).ToList();

            return formsByCreatedDate;
        }

        public IEnumerable<Form> GetByModifiedDate(DateTime modifiedDate)
        {
            IEnumerable<Form> formsByModifiedData = databaseContext.Forms.Where(form => form.ModifiedDate == modifiedDate).ToList();

            return formsByModifiedData;
        }

        #endregion

        #region getData

        public Section GetSection(int formIdentifier, int sectionIdentifier)
        {
            Form form = databaseContext.Forms.Find(formIdentifier);
            Section formSection = form.Sections.ToList().Find(section => section.Id == sectionIdentifier);

            return formSection;
        }

        public IEnumerable<Criteria> GetSectionCriteria(int formIdentifier, int sectionIdentifier)
        {
            Form form = databaseContext.Forms.Find(formIdentifier);
            Section formSection = form.Sections.ToList().Find(section => section.Id == sectionIdentifier);
            IEnumerable<Criteria> sectionCriteria = formSection.Criteria;

            return sectionCriteria;
        }

        public Criteria GetSectionCriterion(int formIdentifier, int sectionIdentifier, int criterionIdentifier)
        {
            Form form = databaseContext.Forms.Find(formIdentifier);
            Section formSection = form.Sections.ToList().Find(section => section.Id == sectionIdentifier);
            Criteria sectionCriterion = formSection.Criteria.ToList().Find(criterion => criterion.Id == criterionIdentifier);

            return sectionCriterion;
        }

        public IEnumerable<Section> GetSectionsByEvaluationScale(int formIdentifier, int evaluationScaleIdentifier)
        {
            Form form = databaseContext.Forms.Find(formIdentifier);
            List<Section> formSections = new List<Section>();

            foreach (var section in form.Sections)
            {
                if (section.EvaluationScale.Id == evaluationScaleIdentifier)
                {
                    formSections.Add(section);
                }
            }

            return formSections;
        }

        public IEnumerable<Importance> GetImportances()
        {
            return databaseContext.Importances;
        }

        public IEnumerable<Status> GetStatuses()
        {
            return databaseContext.Statuses;
        }

        #endregion
    }
}
