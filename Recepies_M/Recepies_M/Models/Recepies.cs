using System;
using System.Collections.Generic;
using System.Text;
using Recepies_M.Settings;

namespace Recepies_M.Models
{
    public class Recepies
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }
        //public string FullImage => AppSettings.ApiUrlIMG + imageUrl.Remove(0,1);
        public string FullImage => AppSettings.ApiUrlIMG + imageUrl;
        public int preparationTime { get; set; }
        public int cookingTime { get; set; }
        public int people { get; set; }
        public int UserId { get; set; }
        public string difficulty { get; set; }
        public string category { get; set; }
       // public byte[] Image { get; set; }
        public List<Ingredient> ingredients { get; set; }
        public List<PreparationStep> preparationSteps { get; set; }
    }
}
