using System;
using System.Collections.Generic;
using System.Text;

namespace Recepies_M.Models
{
    public class RecepiesPartial
    {
        public int id { get; set; }
        public string title { get; set; }
        public string imageUrl { get; set; }
        public int preparationTime { get; set; }
        public string category { get; set; }
        public string difficulty { get; set; }
        public int people { get; set; }
    }
}
