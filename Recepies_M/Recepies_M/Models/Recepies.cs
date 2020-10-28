using System;
using System.Collections.Generic;
using System.Text;

namespace Recepies_M.Models
{
    public class Recepies
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }
        public int preparationTime { get; set; }
        public int cookingTime { get; set; }
        public int people { get; set; }
        public string difficulty { get; set; }
        public string category { get; set; }
        public List<Ingredient> ingredients { get; set; }
        public List<PreparationStep> preparationSteps { get; set; }
    }
}
