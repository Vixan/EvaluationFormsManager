﻿using EvaluationFormsManager.Domain;
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

        public IEnumerable<Form> GetCreatedBy(int employeeIdentifier)
        {
            return forms.FindAll(evaluation => evaluation.CreatedBy == employeeIdentifier);
        }

        public IEnumerable<Form> GetModifiedBy(int employeeIdentifier)
        {
            return forms.FindAll(evaluation => evaluation.ModifiedBy == employeeIdentifier);
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

        public IEnumerable<Section> GetSectionsByEvaluationScale(int formIdentifier, int evaluationScaleIdentifier)
        {
            return forms.Find(form => form.Id == formIdentifier)
                .Sections.Where(section => section.EvaluationScale.Id == evaluationScaleIdentifier);
        }

        public IEnumerable<Form> GetUnavailable()
        {
            return forms.FindAll(evaluation => evaluation.Status == false);
        }

        public void Save()
        {
            // No save method implementation for in-memory persistence.
        }
    }
}
