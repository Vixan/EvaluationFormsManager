using EvaluationFormsManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvaluationFormsManager.Persistence.Memory
{
    public class MemFormRepository : IFormRepository
    {
        private List<Form> forms = new List<Form>();

        public void Add(Form entity)
        {
            forms.Add(entity);
        }

        public void Delete(Form entity)
        {
            forms.Remove(entity);
        }

        public IEnumerable<Form> GetAll()
        {
            return forms;
        }

        public IEnumerable<Form> GetAvailable()
        {
            return forms.FindAll(form => form.Status.Name == "Enabled");
        }

        public IEnumerable<Form> GetByCreatedDate(DateTime createdDate)
        {
            return forms.FindAll(form => form.CreatedDate == createdDate);
        }

        public Form GetById(int identifier)
        {
            return forms.Find(form => form.Id == identifier);
        }

        public IEnumerable<Form> GetByModifiedDate(DateTime modifiedDate)
        {
            return forms.FindAll(form => form.ModifiedDate == modifiedDate);
        }

        public Form GetByName(string formName)
        {
            return forms.Find(form => form.Name == formName);
        }

        public IEnumerable<Form> GetCreatedBy(int employeeIdentifier)
        {
            return forms.FindAll(form => form.CreatedBy == employeeIdentifier);
        }

        public IEnumerable<Importance> GetImportances()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Form> GetModifiedBy(int employeeIdentifier)
        {
            return forms.FindAll(form => form.ModifiedBy == employeeIdentifier);
        }

        public Section GetSection(int formIdentifier, int sectionIdentifier)
        {
            return forms.Find(form => form.Id == formIdentifier)
                .Sections.Where(section => section.Id == sectionIdentifier).FirstOrDefault();
        }

        public IEnumerable<Criteria> GetSectionCriteria(int formIdentifier, int sectionIdentifier)
        {
            return forms.Find(form => form.Id == formIdentifier)
                .Sections.Where(section => section.Id == sectionIdentifier).FirstOrDefault().Criteria;
        }

        public Criteria GetSectionCriterion(int formIdentifier, int sectionIdentifier, int criterionIdentifier)
        {
            return forms.Find(form => form.Id == formIdentifier)
                .Sections.Where(section => section.Id == sectionIdentifier).FirstOrDefault()
                .Criteria.Where(criterion => criterion.Id == criterionIdentifier).FirstOrDefault();
        }

        public IEnumerable<Section> GetSections()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Section> GetSectionsByEvaluationScale(int formIdentifier, int evaluationScaleIdentifier)
        {
            return forms.Find(form => form.Id == formIdentifier)
                .Sections.Where(section => (int)section.EvaluationScale == evaluationScaleIdentifier);
        }

        public IEnumerable<Status> GetStatuses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Form> GetUnavailable()
        {
            return forms.FindAll(form => form.Status.Name == "Disabled");
        }

        public void Save()
        {
            // No save method implementation for in-memory persistence.
        }
    }
}
