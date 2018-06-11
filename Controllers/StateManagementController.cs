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
using productionorderservice.Validation;
using securityfilter;

namespace productionorderservice.Controllers {
    [Route ("api/productionorders/statemanagement")]
    public class StateManagementController : Controller {
        private readonly IStateManagementService _stateManagementService;
        public StateManagementController (IStateManagementService stateManagementService) {
            _stateManagementService = stateManagementService;
        }

        [HttpPut ("id/")]
        [SecurityFilter ("production_order__allow_update")]
        public async Task<IActionResult> UpdateById ([FromQuery] int productionorderid, [FromQuery] string state,[FromBody] ProductionOrder productionOrder) {

            stateEnum newState = stateEnum.created;
            if (!Enum.TryParse (state, out newState))
                return BadRequest ("State Not Found");

            var productionOrders = await _stateManagementService.setProductionOrderToStatusById (productionorderid, newState,productionOrder);
            if (productionOrders == null)
                return BadRequest ("State Change not Allowed By Configuration");
            return Ok (productionOrders);
        }

        [HttpPut ("number/")]
        [SecurityFilter ("production_order__allow_update")]
        public async Task<IActionResult> UpdateByNumber ([FromQuery] string productionordernumber, [FromQuery] string state) {

            stateEnum newState = stateEnum.created;
            if (!Enum.TryParse (state, out newState))
                return BadRequest ("State Not Found");
            var productionOrders = await _stateManagementService.setProductionOrderToStatusByNumber (productionordernumber, newState);
            if (productionOrders == null)
                return BadRequest ("State Change not Allowed By Configuration");
            return Ok (productionOrders);
        }
    }
}