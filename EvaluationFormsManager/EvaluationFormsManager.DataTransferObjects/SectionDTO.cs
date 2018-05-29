using EvaluationFormsManager.Domain;
using System.Collections.Generic;

namespace EvaluationFormsManager.DataTransferObjects
{
    public class SectionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Criteria> Criteria { get; set; }
        public EvaluationScale EvaluationScale { get; set; }
    }
}
