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
    public class ProductionOrdersController : Controller {
        private readonly IProductionOrderService _productionOrderService;
        public ProductionOrdersController (IProductionOrderService productionOrderService) {
            _productionOrderService = productionOrderService;
        }

        [HttpGet]
        [ResponseCache (CacheProfileName = "productionordercache")]
        [SecurityFilter ("production_order__allow_read")]
        public async Task<IActionResult> Get ([FromQuery] int startat, [FromQuery] int quantity, [FromQuery] string fieldFilter, [FromQuery] string fieldValue, [FromQuery] string orderField, [FromQuery] string order) {
            List<string> fields = new List<string> ();
            fields.Add (fieldFilter + "," + fieldValue);
            var orderFieldEnum = ProductionOrderFields.Default;
            Enum.TryParse (orderField, true, out orderFieldEnum);
            var orderEnumValue = OrderEnum.Ascending;
            Enum.TryParse (order, true, out orderEnumValue);
            if (quantity == 0)
                quantity = 50;
            var (productionOrders, total) = await _productionOrderService.getProductionOrders (startat, quantity,
                fields, orderFieldEnum, orderEnumValue);
            return Ok (new { values = productionOrders, total = total });
        }

        [HttpGet ("v2")]
        [ResponseCache (CacheProfileName = "productionordercache")]
        [SecurityFilter ("production_order__allow_read")]
        public async Task<IActionResult> Get ([FromQuery] int startat, [FromQuery] int quantity, [FromQuery] List<string> filters, [FromQuery] string orderField, [FromQuery] string order) {
            var orderFieldEnum = ProductionOrderFields.Default;
            Enum.TryParse (orderField, true, out orderFieldEnum);
            var orderEnumValue = OrderEnum.Ascending;
            Enum.TryParse (order, true, out orderEnumValue);
            if (quantity == 0)
                quantity = 50;
            var (productionOrders, total) = await _productionOrderService.getProductionOrders (startat, quantity,
                filters, orderFieldEnum, orderEnumValue);
            return Ok (new { values = productionOrders, total = total });
        }

        public async Task<IActionResult> Get ([FromQuery] List<int> filters) {
            
            return Ok ();
        }


        [HttpGet ("{id}")]
        [SecurityFilter ("production_order__allow_read")]
        public async Task<IActionResult> Get (int id) {
            var productionOrder = await _productionOrderService.getProductionOrder (id);
            if (productionOrder == null)
                return NotFound ();
            return Ok (productionOrder);
        }

        [HttpGet ("thing/{thingid}")]
        [SecurityFilter ("production_order__allow_read")]
        public async Task<IActionResult> GetThindId (int? thingid) {
            if (thingid == null)
                return NotFound ();
            var productionOrder = await _productionOrderService.getProductionOrderOnThing (thingid.Value);
            if (productionOrder == null)
                return NotFound ();
            return Ok (productionOrder);
        }

        [HttpPost]
        [SecurityFilter ("production_order__allow_update")]
        public async Task<IActionResult> Post ([FromBody] ProductionOrder productionOrder) {
            productionOrder.productionOrderId = 0;
            if (ModelState.IsValid) {
                bool pOExists = await _productionOrderService.checkProductionOrderNumber (productionOrder.productionOrderNumber);
                if (pOExists) {
                    ModelState.AddModelError ("productionOrderNumber", "This Production Order Number already exists.");
                    return BadRequest (ModelState);
                }
                bool pOTypeExists = await _productionOrderService.checkProductionOrderType (productionOrder.productionOrderTypeId.Value);
                if (!pOTypeExists) {
                    ModelState.AddModelError ("productionOrderTypeId", "This Production Order Type Doesn't exists.");
                    return BadRequest (ModelState);
                }

                productionOrder = await _productionOrderService.addProductionOrder (productionOrder);
                if (productionOrder == null)
                    return BadRequest ();
                return Created ($"api/phases/{productionOrder.productionOrderId}", productionOrder);
            }
            return BadRequest (ModelState);
        }

    }
}