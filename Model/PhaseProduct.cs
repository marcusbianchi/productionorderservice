using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace productionorderservice.Model
{
    public enum PhaseProductType
    {
        scrap,
        finished,
        semi_finished,
        contaminent,
        base_product
    }
    public class PhaseProduct
    {
        [Key]
        [JsonIgnore]
        public int internalId { get; set; }
        [Required]
        public double minValue { get; set; }
        [Required]
        public double maxValue { get; set; }
        [Required]
        [MaxLength(50)]
        public string measurementUnit { get; set; }
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public PhaseProductType phaseProductType { get; set; }
        public Product product { get; set; }
    }
}
