using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace productionorderservice.Model
{
    public class Tag
    {
        [Key]
        [JsonIgnore]
        public int internalId { get; set; }
        public int tagId { get; set; }
        public string tagName { get; set; }
        public string tagDescription { get; set; }
        public int thingGroupId { get; set; }
        public string tagGroup{get;set;}
        public ThingGroup thingGroup { get; set; }

    }
}