using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using productionorderservice.Model;
using productionorderservice.Services.Interfaces;

namespace productionorderservice.Services
{
    public class RecipeService : IRecipeService
    {
        private IConfiguration _configuration;
        private HttpClient client = new HttpClient();
        private readonly IRecipePhaseService _recipePhaseService;
        public RecipeService(IConfiguration configuration, IRecipePhaseService recipePhaseService)
        {
            _configuration = configuration;
            _recipePhaseService = recipePhaseService;
        }

        public async Task<Recipe> getRecipe(int recipeId)
        {
            try
            {
                Recipe returnRecipe = null;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var builder = new UriBuilder(_configuration["recipeServiceEndpoint"] + "/api/recipes/" + recipeId);
                string url = builder.ToString();
                var result = await client.GetAsync(url);
                switch (result.StatusCode)
                {
                    case HttpStatusCode.OK:
                        returnRecipe = JsonConvert.DeserializeObject<Recipe>(await client.GetStringAsync(url));
                        returnRecipe.phases = await _recipePhaseService.getPhasesFromRecipe(returnRecipe.recipeId);
                        return returnRecipe;
                    case HttpStatusCode.NotFound:
                        return returnRecipe;
                    case HttpStatusCode.InternalServerError:
                        return returnRecipe;
                }
                return returnRecipe;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<List<Recipe>> getRecipes(int startat, int quantity)
        {
            try
            {
                List<Recipe> returnRecipes = null;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var builder = new UriBuilder(_configuration["recipeServiceEndpoint"] + "/api/recipes");
                var query = HttpUtility.ParseQueryString(builder.Query);
                if (startat != 0)
                    query["startat"] = startat.ToString();
                if (quantity != 0)
                    query["quantity"] = quantity.ToString();
                builder.Query = query.ToString();
                string url = builder.ToString();
                var result = await client.GetAsync(url);
                switch (result.StatusCode)
                {
                    case HttpStatusCode.OK:
                        returnRecipes = JsonConvert.DeserializeObject<List<Recipe>>(await client.GetStringAsync(url));
                        foreach (var returnRecipe in returnRecipes)
                        {
                            returnRecipe.phases = await _recipePhaseService.getPhasesFromRecipe(returnRecipe.recipeId);
                        }
                        return returnRecipes;
                    case HttpStatusCode.NotFound:
                        return returnRecipes;
                    case HttpStatusCode.InternalServerError:
                        return returnRecipes;
                }
                return returnRecipes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}