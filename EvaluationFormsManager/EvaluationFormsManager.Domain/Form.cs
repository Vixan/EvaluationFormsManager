using System;
using System.Collections.Generic;

namespace EvaluationFormsManager.Domain
{
    public class Form
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Importance Importance { get; set; }
        public Status Status { get; set; }
        public virtual ICollection<Section> Sections { get; set; }

        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
