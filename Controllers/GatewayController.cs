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
using securityfilter;

namespace productionorderservice.Controllers {
    [Route ("")]
    public class GatewayController : Controller {
        private IConfiguration _configuration;
        private readonly IRecipeService _recipeService;
        private readonly IThingGroupService _thingGroupService;

        public GatewayController (IConfiguration configuration,
            IRecipeService recipeService, IThingGroupService thingGroupService) {
            _configuration = configuration;
            _recipeService = recipeService;
            _thingGroupService = thingGroupService;

        }

        [HttpGet ("gateway/recipes/")]
        [Produces ("application/json")]
        [SecurityFilter ("production_order__allow_read")]
        public async Task<IActionResult> GetRecipes ([FromQuery] int startat, [FromQuery] int quantity, [FromQuery] string fieldFilter, [FromQuery] string fieldValue, [FromQuery] string orderField, [FromQuery] string order) {

            var recipes = await _recipeService.getRecipes (startat, quantity, fieldFilter,
                fieldValue, orderField, order);
            return Ok (recipes);
        }

        [HttpGet ("gateway/recipes/{id}")]
        [Produces ("application/json")]
        [SecurityFilter ("production_order__allow_read")]
        public async Task<IActionResult> GetRecipe (int id) {
            var recipe = await _recipeService.getRecipe (id);
            return Ok (recipe);
        }

        [HttpGet ("gateway/thinggroups/")]
        [Produces ("application/json")]
        [SecurityFilter ("production_order__allow_read")]
        public async Task<IActionResult> GetGroups ([FromQuery] int startat, [FromQuery] int quantity, [FromQuery] string fieldFilter, [FromQuery] string fieldValue, [FromQuery] string orderField, [FromQuery] string order) {

            var (thingGroups, resultCode) = await _thingGroupService.getGroups (startat, quantity, fieldFilter,
                fieldValue, orderField, order);
            switch (resultCode) {
                case HttpStatusCode.OK:
                    return Ok (thingGroups);
                case HttpStatusCode.NotFound:
                    return NotFound ();
            }
            return StatusCode (StatusCodes.Status500InternalServerError);
        }

        [HttpGet ("gateway/thinggroups/{id}")]
        [Produces ("application/json")]
        [SecurityFilter ("production_order__allow_read")]
        public async Task<IActionResult> GetGroup (int id) {
            var (thingGroup, resultCode) = await _thingGroupService.getGroup (id);
            switch (resultCode) {
                case HttpStatusCode.OK:
                    return Ok (thingGroup);
                case HttpStatusCode.NotFound:
                    return NotFound ();
            }
            return StatusCode (StatusCodes.Status500InternalServerError);
        }

        [HttpGet ("gateway/thinggroups/attachedthings/{groupid}")]
        [Produces ("application/json")]
        [SecurityFilter ("production_order__allow_read")]
        public async Task<IActionResult> GetAttachedThings (int groupid) {
            var (things, resultCode) = await _thingGroupService.GetAttachedThings (groupid);
            switch (resultCode) {
                case HttpStatusCode.OK:
                    return Ok (things);
                case HttpStatusCode.NotFound:
                    return NotFound ();
            }
            return StatusCode (StatusCodes.Status500InternalServerError);
        }
    }
}