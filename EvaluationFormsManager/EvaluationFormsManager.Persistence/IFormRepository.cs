﻿using EvaluationFormsManager.Domain;
using System.Collections.Generic;

namespace EvaluationFormsManager.Persistence
{
    public interface IFormRepository : IRepository<Form>
    {
        Form GetByName(string formName);
        IEnumerable<Form> GetOwned(string userIdentifier);
        IEnumerable<Form> GetShared(string userIdentifier);
        void Share(Form formToShare, IEnumerable<string> shareWithUsers);
        void Unshare(Form formToUnshare, IEnumerable<string> unshareWithUsers);

        Section GetSection(int sectionIdentifier);

        IEnumerable<Importance> GetImportances();
        IEnumerable<Status> GetStatuses();
        IEnumerable<Section> GetSections();

        void AddImportance(Importance importance);
        void AddStatus(Status status);
    }
}
