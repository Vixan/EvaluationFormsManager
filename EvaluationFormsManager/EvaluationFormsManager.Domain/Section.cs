using System;
using System.Collections.Generic;

namespace EvaluationFormsManager.Domain
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Criteria> Criteria { get; set; }
        public EvaluationScale EvaluationScale { get; set; }

        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
