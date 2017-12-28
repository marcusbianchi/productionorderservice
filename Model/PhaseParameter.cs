using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace productionorderservice.Model
{
    public class PhaseParameter
    {
        [Key]
        [JsonIgnore]
        public int internalId { get; set; }
        [Required]
        [MaxLength(50)]
        public string setupValue { get; set; }
        [Required]
        [MaxLength(50)]
        public string measurementUnit { get; set; }
        [MaxLength(50)]
        public string minValue { get; set; }
        [MaxLength(50)]
        public string maxValue { get; set; }
        [NotMapped]
        public Tag tag { get; set; }
    }
}