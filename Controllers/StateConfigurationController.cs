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
    public class StateConfigurationController : Controller {
        private readonly IStateConfigurationService _stateConfigurationService;
        public StateConfigurationController (IStateConfigurationService stateConfigurationService) {
            _stateConfigurationService = stateConfigurationService;
        }

        [HttpGet ("{productionOrderTypeId}")]
        [SecurityFilter ("production_order__allow_read")]
        public async Task<IActionResult> Get (int productionOrderTypeId) {
            var stateConfiguration = await _stateConfigurationService.getStateConfiguration (productionOrderTypeId);
            if (stateConfiguration == null)
                return NotFound ();
            return Ok (stateConfiguration);
        }

        [HttpPut ("{productionOrderTypeId}")]
        [SecurityFilter ("production_order__allow_update")]
        public async Task<IActionResult> Put (int productionOrderTypeId, [FromBody] StateConfiguration stateConfiguration) {
            if (ModelState.IsValid) {
                stateConfiguration = await _stateConfigurationService.updateProductionOrderType (productionOrderTypeId, stateConfiguration);
                if (stateConfiguration != null) {
                    return NoContent ();
                }
                return NotFound ();
            }
            return BadRequest (ModelState);
        }

        [HttpDelete ("{productionOrderTypeId}")]
        [SecurityFilter ("production_order__allow_update")]
        public async Task<IActionResult> Delete (int productionOrderTypeId) {
            if (ModelState.IsValid) {
                var productionOrderType = await _stateConfigurationService.deleteProductionOrderType (productionOrderTypeId);
                if (productionOrderType == true) {
                    return NoContent ();
                }
                return NotFound ();
            }
            return BadRequest (ModelState);
        }
    }
}