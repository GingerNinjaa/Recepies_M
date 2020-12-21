using System;
using System.Collections.Generic;
using System.Text;

namespace Recepies_M.Models
{
     public class Ingredient
    {
        public int id { get; set; }
        public string name { get; set; }
        public double amount { get; set; }
        public string amountDesc { get; set; }
        public int recipeId { get; set; }
    }
}
