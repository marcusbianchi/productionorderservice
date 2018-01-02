using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}