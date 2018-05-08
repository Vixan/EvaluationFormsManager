using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationFormsManager.Domain
{
    public class EvaluationScale
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<EvaluationScaleOption> EvaluationScaleOptions { get; set; }
    }
}
