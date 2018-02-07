using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace productionorderservice.Model
{
    public class Phase
    {
        [Key]
        [JsonIgnore]
        public int internalId { get; set; }
        public int phaseId{get;set;}
        [Required]
        [MaxLength(50)]
        public string phaseName { get; set; }
        [MaxLength(100)]
        public string phaseCode { get; set; }
        public ICollection<PhaseProduct> phaseProducts { get; set; }
        public ICollection<PhaseParameter> phaseParameters { get; set; }
    }
}