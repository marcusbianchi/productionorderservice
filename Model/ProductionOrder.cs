using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace productionorderservice.Model
{
    public class ProductionOrder
    {
        public int productionOrderId { get; set; }
        [Required]
        public Recipe recipe { get; set; }
        [Required]
        [MaxLength(50)]
        public string productionOrderNumber { get; set; }
        [Required]
        public ProductionOrderType productionOrderType { get; set; }
    }
}