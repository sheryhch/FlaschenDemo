using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlaschenDemo.Core.DTOs.Beer
{
    public class BeerViewModel
    {
        public class Beer
        {
            public int id { get; set; }
            public string brandName { get; set; } = "";
            public string name { get; set; } = "";
            public Article[] articles { get; set; }
            public string descriptionText { get; set; } = "";
        }

        public class Article
        {
            public int id { get; set; }
            public string shortDescription { get; set; } = "";
            public float price { get; set; }
            public string unit { get; set; } = "";
            public string pricePerUnitText { get; set; } = "";
            public string image { get; set; } = "";
        }



    }
}
