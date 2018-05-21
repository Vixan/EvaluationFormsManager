using EvaluationFormsManager.Domain;
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
            IEnumerable<Form> forms = databaseContext.Forms;

            return forms;
        }

        public override Form GetById(int identifier)
        {
            Form foundForm = databaseContext.Forms
                .ToList()
                .Find(form => form.Id == identifier);

            return foundForm;
        }

        public Form GetByName(string formName)
        {
            Form formByName = databaseContext.Forms.Where(form => form.Name == formName).FirstOrDefault();

            return formByName;
        }

        public Section GetSection(int sectionIdentifier)
        {
            Section formSection = databaseContext.Sections.ToList().Find(section => section.Id == sectionIdentifier);

            return formSection;
        }

        public IEnumerable<Importance> GetImportances()
        {
            return databaseContext.Importances;
        }

        public IEnumerable<Status> GetStatuses()
        {
            return databaseContext.Statuses;
        }

        public IEnumerable<Section> GetSections()
        {
            return databaseContext.Sections;
        }
    }
}
