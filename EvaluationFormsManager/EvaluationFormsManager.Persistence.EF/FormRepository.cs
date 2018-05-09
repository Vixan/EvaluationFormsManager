using EvaluationFormsManager.Domain;
using System;
using System.Collections.Generic;

namespace EvaluationFormsManager.Persistence.EF
{
    class FormRepository : IFormRepository
    {
        private readonly DatabaseContext databaseContext = null; 

        public FormRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Add(Form entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Form entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Form> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Form> GetAVailable()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Form> GetByCreatedDate(DateTime createdDate)
        {
            throw new NotImplementedException();
        }

        public Form GetById(int identifier)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Form> GetByModifiedDate(DateTime modifiedDate)
        {
            throw new NotImplementedException();
        }

        public Form GetByName(string formName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Form> GetCreatedBy(int employeeIdentifier)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Form> GetModifiedBy(int employeeIdentifier)
        {
            throw new NotImplementedException();
        }

        public Section GetSection(int formIdentifier, int sectionIdentifier)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Criteria> GetSectionCriteria(int formIdentifier, int sectionIdentifier)
        {
            throw new NotImplementedException();
        }

        public Criteria GetSectionCriterion(int formIdentifier, int sectionIdentifier, int criterionIdentifier)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Section> GetSectionsByEvaluationScale(int formIdentifier, int evaluationScaleIdentifier)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Form> GetUnavailable()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
