using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using productionorderservice.Validation;

namespace productionorderservice.Model
{
    public class ProductionOrderType
    {
        public int productionOrderTypeId { get; set; }
        [Required]
        [MaxLength(50)]
        public string typeDescription { get; set; }
        [Required]
        [MaxLength(50)]
        public string typeScope { get; set; }
        public StateConfiguration stateConfiguration { get; set; }
        [NotMapped]
        public IList<ThingGroup> thingGroups { get; set; }
        [Column("thingGroupIds", TypeName = "integer[]")]
        public int[] thingGroupIds { get; set; }

    }
}