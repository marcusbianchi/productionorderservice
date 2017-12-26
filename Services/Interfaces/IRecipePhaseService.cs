using System.Collections.Generic;
using System.Threading.Tasks;
using productionorderservice.Model;

namespace productionorderservice.Services.Interfaces
{
    public interface IRecipePhaseService
    {
        Task<List<Phase>> getPhasesFromRecipe(int recipeId);
    }
}