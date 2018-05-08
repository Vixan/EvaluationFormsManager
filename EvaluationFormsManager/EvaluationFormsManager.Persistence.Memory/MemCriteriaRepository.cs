using System;
using System.Collections.Generic;
using System.Text;
using EvaluationFormsManager.Domain;

namespace EvaluationFormsManager.Persistence.Memory
{
    public class MemCriteriaRepository : ICriteriaRepository
    {
        private List<Criteria> criteria = new List<Criteria>();

        public void Add(Criteria entity)
        {
            criteria.Add(entity);
        }

        public void Delete(Criteria entity)
        {
            criteria.Remove(entity);
        }

        public IEnumerable<Criteria> GetAll()
        {
            return criteria;
        }

        public IEnumerable<Criteria> GetByCreatedDate(DateTime createdDate)
        {
            return criteria.FindAll(criteria => criteria.CreatedDate == createdDate);
        }

        public IEnumerable<Criteria> GetByEvaluationScaleOption(int evaluationScaleOptionIdentifier)
        {
            return criteria.FindAll(criteria => criteria.Grade.Id == evaluationScaleOptionIdentifier);
        }

        public Criteria GetById(int identifier)
        {
            return criteria.Find(criteria => criteria.Id == identifier);
        }

        public IEnumerable<Criteria> GetByModifiedDate(DateTime modifiedDate)
        {
            return criteria.FindAll(criteria => criteria.ModifiedDate == modifiedDate);
        }

        public Criteria GetByName(string criteriaName)
        {
            return criteria.Find(criteria => criteria.Name == criteriaName);
        }

        public IEnumerable<Criteria> GetCreatedBy(int userIdentifier)
        {
            return criteria.FindAll(criteria => criteria.CreatedBy == userIdentifier);
        }

        public IEnumerable<Criteria> GetModifiedBy(int userIdentifier)
        {
            return criteria.FindAll(criteria => criteria.ModifiedBy == userIdentifier);
        }

        public void Save()
        {
            // No save method implementation for in-memory persistence.
        }
    }
}
