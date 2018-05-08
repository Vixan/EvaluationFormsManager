using EvaluationFormsManager.Domain;
using System;
using System.Collections.Generic;

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

        public IEnumerable<Form> GetAVailable()
        {
            return forms.FindAll(evaluation => evaluation.Status == true);
        }

        public IEnumerable<Form> GetByCreatedDate(DateTime createdDate)
        {
            return forms.FindAll(evaluation => evaluation.CreatedDate == createdDate);
        }

        public Form GetById(int identifier)
        {
            return forms.Find(evaluation => evaluation.Id == identifier);
        }

        public IEnumerable<Form> GetByModifiedDate(DateTime modifiedDate)
        {
            return forms.FindAll(evaluation => evaluation.ModifiedDate == modifiedDate);
        }

        public Form GetByName(string evaluationName)
        {
            return forms.Find(evaluation => evaluation.Name == evaluationName);
        }

        public IEnumerable<Form> GetCompleted()
        {
            return forms.FindAll(evaluation => evaluation.IsCompleted == true);
        }

        public IEnumerable<Form> GetCreatedBy(int userIdentifier)
        {
            return forms.FindAll(evaluation => evaluation.CreatedBy == userIdentifier);
        }

        public IEnumerable<Form> GetModifiedBy(int userIdentifier)
        {
            return forms.FindAll(evaluation => evaluation.ModifiedBy == userIdentifier);
        }

        public IEnumerable<Form> GetUnavailable()
        {
            return forms.FindAll(evaluation => evaluation.Status == false);
        }

        public IEnumerable<Form> GetUncompleted()
        {
            return forms.FindAll(evaluation => evaluation.IsCompleted == false);
        }

        public void Save()
        {
            // No save method implementation for in-memory persistence.
        }
    }
}
