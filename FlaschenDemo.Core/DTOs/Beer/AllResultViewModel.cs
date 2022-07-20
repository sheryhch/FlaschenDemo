using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlaschenDemo.Core.DTOs.Beer
{
    public class AllResultViewModel
    {
        public MinMaxPricePerLiterViewModel MinMaxPricePerLiter { get; set; }
        public List<BeerViewModel.Beer> ExactPriceList { get; set; }
        public List<BeerViewModel.Beer> MostBottlesList { get; set; }

    }
}
