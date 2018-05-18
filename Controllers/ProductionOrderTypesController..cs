using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using productionorderservice.Model;
using productionorderservice.Services.Interfaces;
using securityfilter;

namespace productionorderservice.Controllers {
    [Route ("api/[controller]")]
    public class ProductionOrderTypesController : Controller {
        private readonly IProductionOrderTypeService _productionOrderTypeService;
        public ProductionOrderTypesController (IProductionOrderTypeService productionOrderTypeService) {
            _productionOrderTypeService = productionOrderTypeService;
        }

        [HttpGet]
        [ResponseCache (CacheProfileName = "productionordercache")]
        [SecurityFilter ("production_order__allow_read")]
        public async Task<IActionResult> Get ([FromQuery] int startat, [FromQuery] int quantity) {
            if (quantity == 0)
                quantity = 50;
            var productionOrderTypes = await _productionOrderTypeService.getProductionOrderTypes (startat, quantity);
            return Ok (productionOrderTypes);
        }

        [HttpGet ("{id}")]
        [ResponseCache (CacheProfileName = "productionordercache")]
        [SecurityFilter ("production_order__allow_read")]
        public async Task<IActionResult> Get (int id) {
            var productionOrderType = await _productionOrderTypeService.getProductionOrderType (id);
            if (productionOrderType == null)
                return NotFound ();
            return Ok (productionOrderType);
        }

        [HttpPost]
        [SecurityFilter ("production_order__allow_update")]
        public async Task<IActionResult> Post ([FromBody] ProductionOrderType productionOrderType) {
            productionOrderType.productionOrderTypeId = 0;
            if (ModelState.IsValid) {
                productionOrderType = await _productionOrderTypeService.addProductionOrderType (productionOrderType);
                return Created ($"api/phases/{productionOrderType.productionOrderTypeId}", productionOrderType);
            }
            return BadRequest (ModelState);
        }

        [HttpPut ("{id}")]
        [SecurityFilter ("production_order__allow_update")]
        public async Task<IActionResult> Put (int id, [FromBody] ProductionOrderType productionOrderType) {
            if (ModelState.IsValid) {
                productionOrderType = await _productionOrderTypeService.updateProductionOrderType (id, productionOrderType);
                if (productionOrderType != null) {
                    return NoContent ();
                }
                return NotFound ();
            }
            return BadRequest (ModelState);
        }

        [HttpDelete ("{id}")]
        [SecurityFilter ("production_order__allow_update")]
        public async Task<IActionResult> Delete (int id) {
            if (ModelState.IsValid) {
                var productionOrderType = await _productionOrderTypeService.deleteProductionOrderType (id);
                if (productionOrderType != null) {
                    return NoContent ();
                }
                return NotFound ();
            }
            return BadRequest (ModelState);
        }

    }
}