using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using productionorderservice.Validation;

namespace productionorderservice.Model
{


    public class StateConfiguration
    {
        [JsonIgnore]
        public int stateConfigurationId { get; set; }
        [Required]
        public int productionOrderTypeId { get; set; }
        [StatusValidation]
        public ICollection<ConfiguredState> states { get; set; }
    }
}