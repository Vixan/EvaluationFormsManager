using EvaluationFormsManager.Domain;
using System;
using System.Collections.Generic;

namespace EvaluationFormsManager.Shared
{
    public interface IFormService
    {
        Form GetForm(int formIdentifier);
        IEnumerable<Form> GetAllForms();

        void AddForm(Form form);
        void DeleteForm(Form form);
        void UpdateForm(Form form);

        void DeleteFormSection(int formIdentifier, Section section);

        IEnumerable<Form> GetAllAVailableForms();
        IEnumerable<Form> GetAllUnavailableForms();

        IEnumerable<Form> GetAllFormsByCreatedDate(DateTime createdDate);
        IEnumerable<Form> GetAllFormsByModifiedDate(DateTime modifiedDate);

        IEnumerable<Form> GetAllFormsCreatedBy(int employeeIdentifier);
        IEnumerable<Form> GetAllFormsModifiedBy(int employeeIdentifier);

        IEnumerable<Section> GetAllFormSectionsByEvaluationScale(int formIdentifier, int evaluationScaleIdentifier);
        Section GetFormSection(int formIdentifier, int sectionIdentifier);
        IEnumerable<Criteria> GetAllFormSectionCriteria(int formIdentifier, int sectionIdentifier);
        Criteria GetFormSectionCriterion(int formIdentifier, int sectionIdentifier, int criterionIdentifier);

        IEnumerable<Status> GetAllStatuses();
        IEnumerable<Importance> GetAllImportances();
    }
}
