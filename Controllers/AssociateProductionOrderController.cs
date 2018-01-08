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
using productionorderservice.Services.Interfaces;

namespace productionorderservice.Controllers
{
    [Route("api/productionorders/[controller]")]
    public class AssociateProductionOrderController : Controller
    {
        private readonly IAssociateProductionOrderService _associateProductionOrderService;
        public AssociateProductionOrderController(IAssociateProductionOrderService associateProductionOrderService)
        {
            _associateProductionOrderService = associateProductionOrderService;
        }

        [HttpPut]
        [Produces("application/json")]
        public async Task<IActionResult> GetGroups([FromQuery]int thingId, [FromQuery]int productionOrderId)
        {

            var (PO, result) = await _associateProductionOrderService.AssociateProductionOrder(thingId, productionOrderId);
            if (PO == null)
                return BadRequest(result);
            return Ok(PO);
        }


    }
}