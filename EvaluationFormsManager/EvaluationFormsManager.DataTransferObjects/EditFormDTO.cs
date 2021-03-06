﻿using EvaluationFormsManager.Domain;
using System.Collections.Generic;

namespace EvaluationFormsManager.DataTransferObjects
{
    public class EditFormDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ImportanceId { get; set; }
        public int StatusId { get; set; }
        public ICollection<Section> Sections { get; set; }
    }
}