using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace productionorderservice.Model
{
    public class Recipe
    {
        public int recipeId { get; set; }
        [MaxLength(50)]
        public string recipeName { get; set; }
        [MaxLength(50)]
        public string recipeCode { get; set; }
        public PhaseProduct recipeProduct { get; set; }
        public IList<Phase> phases { get; set; }
    }
}