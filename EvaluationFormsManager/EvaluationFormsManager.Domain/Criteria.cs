using System;

namespace EvaluationFormsManager.Domain
{
    public class Criteria
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
