using FlaschenDemo.Core.DTOs.Beer;
using FlaschenDemo.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlaschenDemo.Test
{
    public class BeerTest
    {
        List<BeerViewModel.Beer> beers;
        public BeerTest()
        {
            beers = new List<BeerViewModel.Beer>()
            {
                 new BeerViewModel.Beer
                    {
                        brandName = "Büble",
                        id = 1138,
                        name = "Allgäuer Büble Bayrisch Hell",
                        articles = new BeerViewModel.Article[]{
                                new BeerViewModel.Article { id=1491, price=17.99f, pricePerUnitText="(1,80 €/Liter)" ,shortDescription="20 x 0,5L (Glas)" } }
                    },
                new BeerViewModel.Beer
                        {
                            brandName = "Alpirsbacher",
                            id = 3080,
                            name = "Alpirsbacher Kloster Helles",
                            articles = new BeerViewModel.Article[]{
                                new BeerViewModel.Article { id=4156, price=20, pricePerUnitText="(2,10 €/Liter)" ,shortDescription="20 x 0,5L (Glas)" } }
                        }
            };
        }


        [Fact]
        public async Task must_pass_GetByMostBottles()
        {
            BeerService _beerService = new BeerService();
            var result= await _beerService.GetByMostBottles(beers);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task must_pass_GetByPrice()
        {
            BeerService _beerService = new BeerService();
            var result = await _beerService.GetByPrice(beers,20);
            Assert.Equal(1, result.Count());
            Assert.Equal(20, result.First().articles.First().price);
        }

        [Fact]
        public async Task must_pass_GetMinMaxPricePerLiter()
        {
            BeerService _beerService = new BeerService();
            var result = await _beerService.GetMinMaxPricePerLiter(beers);
            Assert.Equal(3080, result.expensives.First().id);
            Assert.Equal(1138, result.cheaps.First().id);
        }


      

    }
}
