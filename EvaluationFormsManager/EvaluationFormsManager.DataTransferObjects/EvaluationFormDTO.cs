using EvaluationFormsManager.Domain;
using System;
using System.Collections.Generic;

namespace EvaluationFormsManager.DataTransferObjects
{
    public class EvaluationFormDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Importance Importance { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<Section> Sections { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
