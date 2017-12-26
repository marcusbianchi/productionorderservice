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

namespace productionorderservice.Controllers
{
    [Route("api/[controller]")]
    public class ProductionOrdersController : Controller
    {
        private readonly IProductionOrderService _productionOrderService;
        public ProductionOrdersController(IProductionOrderService productionOrderService)
        {
            _productionOrderService = productionOrderService;
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "productionordercache")]
        public async Task<IActionResult> Get([FromQuery]int startat, [FromQuery]int quantity)
        {
            if (quantity == 0)
                quantity = 50;
            var productionOrders = await _productionOrderService.getProductionOrders(startat, quantity);
            return Ok(productionOrders);
        }

        [HttpGet("{id}")]
        [ResponseCache(CacheProfileName = "productionordercache")]
        public async Task<IActionResult> Get(int id)
        {
            var productionOrder = await _productionOrderService.getProductionOrder(id);
            if (productionOrder == null)
                return NotFound();
            return Ok(productionOrder);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProductionOrder productionOrder)
        {
            productionOrder.productionOrderId = 0;
            if (ModelState.IsValid)
            {
                productionOrder = await _productionOrderService.addProductionOrder(productionOrder);
                return Created($"api/phases/{productionOrder.productionOrderId}", productionOrder);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]ProductionOrder productionOrder)
        {
            if (ModelState.IsValid)
            {
                productionOrder = await _productionOrderService.updateProductionOrder(id, productionOrder);
                if (productionOrder != null)
                {
                    return NoContent();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var productionOrder = await _productionOrderService.deleteProductionOrder(id);
                if (productionOrder != null)
                {
                    return NoContent();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }
    }
}