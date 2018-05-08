using System;

namespace EvaluationFormsManager.Domain
{
    public class Criteria
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        public EvaluationScaleOption Grade { get; set; }
    }
}
