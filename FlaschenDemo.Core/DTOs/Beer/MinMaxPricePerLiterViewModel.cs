using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlaschenDemo.Core.DTOs.Beer
{
    public class MinMaxPricePerLiterViewModel
    {
        public List<BeerViewModel.Beer> expensives { get; set; }
        public List<BeerViewModel.Beer> cheaps { get; set; }
    }
}
