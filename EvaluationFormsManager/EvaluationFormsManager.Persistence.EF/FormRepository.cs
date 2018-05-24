using EvaluationFormsManager.Domain;
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

        public IEnumerable<Form> GetOwned(string userIdentifier)
        {
            IEnumerable<Form> ownedforms = databaseContext.Forms
                .Where(form => form.CreatedBy == userIdentifier);

            return ownedforms;
        }

        public IEnumerable<Form> GetShared(string userIdentifier)
        {
            IEnumerable<Form> sharedForms = databaseContext.SharedForms
                .Where(shared => shared.UserId == userIdentifier)
                .Select(form => form.Form);

            return sharedForms;
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

        public void Share(Form formToShare, IEnumerable<string> shareWithUsers)
        {
            List<SharedForms> sharedForms = new List<SharedForms>();

            shareWithUsers.ToList().ForEach(userId => sharedForms.Add(new SharedForms
            {
                Form = formToShare,
                UserId = userId
            }));

            sharedForms.ForEach(sharedForm => databaseContext.SharedForms.Add(sharedForm));
        }

        public void Unshare(Form formToUnshare, IEnumerable<string> unshareWithUsers)
        {
            unshareWithUsers.ToList().ForEach(userId => 
            {
                SharedForms toDelete = databaseContext
                    .SharedForms
                    .Where(sharedForm => sharedForm.Form.Id == formToUnshare.Id)
                    .Where(sharedForm => sharedForm.UserId == userId)
                    .SingleOrDefault();

                databaseContext.SharedForms.Remove(toDelete);
            });
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

        public void AddImportance(Importance importance)
        {
            databaseContext.Importances.Add(importance);
        }

        public void AddStatus(Status status)
        {
            databaseContext.Statuses.Add(status);
        }
    }
}
