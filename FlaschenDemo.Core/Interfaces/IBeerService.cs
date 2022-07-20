using FlaschenDemo.Core.DTOs.Beer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlaschenDemo.Core.Interfaces
{
    public interface IBeerService
    {

       public  Task<MinMaxPricePerLiterViewModel> GetMinMaxPricePerLiter(string url);

        public Task<List<BeerViewModel.Beer>> GetByPrice(string url, float price);


        public Task<List<BeerViewModel.Beer>> GetByMostBottles(string url);

        public Task<AllResultViewModel> GetAll(string url, float price);


    }
}
