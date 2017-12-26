using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace productionorderservice.Model
{

    public class AdditionalInformation
    {
        [Key]
        [JsonIgnore]
        public int internalId { get; set; }
        public int additionalInformationId { get; set; }
        [MaxLength(50)]
        public string Information { get; set; }
        [MaxLength(50)]
        public string Value { get; set; }
    }
}