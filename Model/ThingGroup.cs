using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace productionorderservice.Model
{
    public class ThingGroup
    {
        [Key]
        [JsonIgnore]
        public int internalId { get; set; }
        public int thingGroupId { get; set; }
        public string groupName { get; set; }
        public string groupCode { get; set; }
    }
}