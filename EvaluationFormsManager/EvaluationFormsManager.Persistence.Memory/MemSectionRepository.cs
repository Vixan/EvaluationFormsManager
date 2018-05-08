using EvaluationFormsManager.Domain;
using System;
using System.Collections.Generic;

namespace EvaluationFormsManager.Persistence.Memory
{
    public class MemSectionRepository : ISectionRepository
    {
        private List<Section> sections = new List<Section>();

        public void Add(Section entity)
        {
            sections.Add(entity);
        }

        public void Delete(Section entity)
        {
            sections.Remove(entity);
        }

        public IEnumerable<Section> GetAll()
        {
            return sections;
        }

        public IEnumerable<Section> GetByCreatedDate(DateTime createdDate)
        {
            return sections.FindAll(section => section.CreatedDate == createdDate);
        }

        public IEnumerable<Section> GetByEvaluationScale(int evaluationScaleIdentifier)
        {
            return sections.FindAll(section => section.EvaluationScale.Id == evaluationScaleIdentifier);
        }

        public Section GetById(int identifier)
        {
            return sections.Find(section => section.Id == identifier);
        }

        public IEnumerable<Section> GetByModifiedDate(DateTime modifiedDate)
        {
            return sections.FindAll(section => section.ModifiedDate == modifiedDate);
        }

        public Section GetByName(string sectionName)
        {
            return sections.Find(section => section.Name == sectionName);
        }

        public IEnumerable<Section> GetCreatedBy(int employeeIdentifier)
        {
            return sections.FindAll(section => section.CreatedBy == employeeIdentifier);
        }

        public IEnumerable<Section> GetModifiedBy(int employeeIdentifier)
        {
            return sections.FindAll(section => section.ModifiedBy == employeeIdentifier);
        }

        public void Save()
        {
            // No save method implementation for in-memory persistence.
        }
    }
}
