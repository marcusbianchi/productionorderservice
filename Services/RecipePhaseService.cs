using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using productionorderservice.Model;
using productionorderservice.Services.Interfaces;

namespace productionorderservice.Services
{
    public class RecipePhaseService : IRecipePhaseService
    {
        private IConfiguration _configuration;
        private HttpClient client = new HttpClient();
        public RecipePhaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<Phase>> getPhasesFromRecipe(int recipeId)
        {
            try
            {
                List<Phase> returnPhases = null;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var builder = new UriBuilder(_configuration["recipeServiceEndpoint"] + "/api/recipes/phases/" + recipeId);
                string url = builder.ToString();
                var result = await client.GetAsync(url);
                switch (result.StatusCode)
                {
                    case HttpStatusCode.OK:
                        returnPhases = JsonConvert.DeserializeObject<List<Phase>>(await client.GetStringAsync(url));
                        return returnPhases;
                    case HttpStatusCode.NotFound:
                        return returnPhases;
                    case HttpStatusCode.InternalServerError:
                        return returnPhases;
                }
                return returnPhases;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}