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
    [Route("")]
    public class GatewayController : Controller
    {
        private IConfiguration _configuration;
        private readonly IRecipeService _recipeService;

        public GatewayController(IConfiguration configuration,
               IRecipeService recipeService)
        {
            _configuration = configuration;
            _recipeService = recipeService;
        }


        [HttpGet("gateway/recipes/")]
        [Produces("application/json")]
        public async Task<IActionResult> GetRecipes([FromQuery]int startat, [FromQuery]int quantity)
        {

            var recipes = await _recipeService.getRecipes(startat, quantity);
            return Ok(recipes);
        }


        [HttpGet("gateway/recipes/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetRecipe(int id)
        {

            var recipe = await _recipeService.getRecipe(id);
            return Ok(recipe);
        }
    }
}