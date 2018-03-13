using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace productionorderservice.Model
{
    public class Recipe
    {
        [Key]
        [JsonIgnore]
        public int internalId { get; set; }
        public int recipeId { get; set; }
        [MaxLength(50)]
        public string recipeName { get; set; }
        public string recipeDescription { get; set; }
        [MaxLength(50)]
        public string recipeCode { get; set; }
        public PhaseProduct recipeProduct { get; set; }
        public ICollection<Phase> phases { get; set; }
    }
}