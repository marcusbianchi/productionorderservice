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
        public async Task<IActionResult> Get([FromQuery]int startat, [FromQuery]int quantity,
            [FromQuery]string fieldFilter, [FromQuery]string fieldValue,
            [FromQuery]string orderField, [FromQuery]string order)
        {
            var fieldFilterEnum = ProductionOrderFields.Default;
            Enum.TryParse(fieldFilter, true, out fieldFilterEnum);
            var orderFieldEnum = ProductionOrderFields.Default;
            Enum.TryParse(orderField, true, out orderFieldEnum);
            var orderEnumValue = OrderEnum.Ascending;
            Enum.TryParse(order, true, out orderEnumValue);
            if (quantity == 0)
                quantity = 50;
            var (productionOrders, total) = await _productionOrderService.getProductionOrders(startat, quantity,
            fieldFilterEnum, fieldValue, orderFieldEnum, orderEnumValue); ;
            return Ok(new { values = productionOrders, total = total });
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
                bool pOExists = await _productionOrderService.checkProductionOrderNumber(productionOrder.productionOrderNumber);
                if (pOExists)
                {
                    ModelState.AddModelError("productionOrderNumber", "This Production Order Number already exists.");
                    return BadRequest(ModelState);
                }
                bool pOTypeExists = await _productionOrderService.checkProductionOrderType(productionOrder.productionOrderTypeId.Value);
                if (!pOTypeExists)
                {
                    ModelState.AddModelError("productionOrderTypeId", "This Production Order Type Doesn't exists.");
                    return BadRequest(ModelState);
                }

                productionOrder = await _productionOrderService.addProductionOrder(productionOrder);
                if (productionOrder == null)
                    return BadRequest();
                return Created($"api/phases/{productionOrder.productionOrderId}", productionOrder);
            }
            return BadRequest(ModelState);
        }




    }
}