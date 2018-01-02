using System.Collections.Generic;
using Newtonsoft.Json;

namespace productionorderservice.Model
{
    public class ConfiguredState
    {
        [JsonIgnore]
        public int configuredStateId { get; set; }
        public string state { get; set; }
        public string[] possibleNextStates { get; set; }
    }
}