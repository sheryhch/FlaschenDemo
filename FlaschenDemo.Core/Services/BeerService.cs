using FlaschenDemo.Core.DTOs.Beer;
using FlaschenDemo.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlaschenDemo.Core.Services
{
    public class BeerService : IBeerService
    {
        HttpClient _httpClient;
        public BeerService()
        {
            _httpClient = new HttpClient();
        }

        //******************************************************************************************************************
        /// <summary>
        /// Which one product comes in the most bottles?
        /// </summary>
        #region GetByMostBottles       
        public async Task<List<BeerViewModel.Beer>> GetByMostBottles(string url)
        {
            try
            {
                // this function fetch data from Api and pass data to GetByMostBottles
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<BeerViewModel.Beer>>(content);
                    var result = await GetByMostBottles(model);
                    return result;
                }
                throw new InvalidOperationException(response.Content.ToString());
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<List<BeerViewModel.Beer>> GetByMostBottles(List<BeerViewModel.Beer> beers)
        {
            if (beers == null) throw new ArgumentException("beers was Null");
            try
            {
               
                // select every beer with max bottles 
                var query = beers.Select(
                    s => new { bottleCounter = s.articles.Select(max => float.Parse(max.shortDescription.Split(" x ")[0])).Max(), beer = s }
                    ).AsQueryable();
                //now try to fetch beer with articles that contain only max bottles
                var result = query.Where(p => p.bottleCounter == query.Max(m => m.bottleCounter))
                    .Select(s =>
                    new BeerViewModel.Beer
                    {
                        brandName = s.beer.brandName,
                        descriptionText = s.beer.descriptionText,
                        id = s.beer.id,
                        name = s.beer.name,
                        // filter articles : choose only valid articles 
                        articles = s.beer.articles.Where(ar => ar.shortDescription.StartsWith(s.bottleCounter + " x ")).ToArray() 
                    })
                    .ToList();
                // return result to Api
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

        //******************************************************************************************************************
        /// <summary>
        /// Which beers cost exactly €17.99?
        /// </summary>
        #region  beers cost exactly
        public async Task<List<BeerViewModel.Beer>> GetByPrice(string url, float price)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<BeerViewModel.Beer>>(content);
                    var result = await GetByPrice(model, price);
                    return result;
                }
                throw new InvalidOperationException(response.Content.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<BeerViewModel.Beer>> GetByPrice(List<BeerViewModel.Beer> beers, float _price)
        {
            if (beers == null) throw new ArgumentException("beers was Null");
            try
            {               
                var query = beers.Where(a => a.articles.Any(b => b.price == _price))
                    .Select(s => new
                    {
                        beer = s,
                        PricePerLiter = s.articles.Where(a => a.price == _price).Select(cheap => float.Parse( cheap.pricePerUnitText.Substring(1, (cheap.pricePerUnitText.IndexOf(" "))) ) ).Min()
                    })
                    .AsQueryable();

                var result = query.OrderBy(o => o.PricePerLiter)
                    .Select(s => new BeerViewModel.Beer 
                    { brandName = s.beer.brandName,
                        descriptionText = s.beer.descriptionText,
                        id = s.beer.id,
                        name = s.beer.name,
                        // filter articles : choose only valid articles that price is _price
                        articles = s.beer.articles.Where(a =>a.price==_price  ).ToArray() 
                    }).ToList();

                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        //******************************************************************************************************************
        /// <summary>
        /// Most expensive and cheapest beer per litre
        /// </summary>   
        #region Most expensive and cheapest beer per litre      
        public async Task<MinMaxPricePerLiterViewModel> GetMinMaxPricePerLiter(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<BeerViewModel.Beer>>(content);
                    var result = await GetMinMaxPricePerLiter(model);
                    return result;
                }
                throw new InvalidOperationException(response.Content.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<MinMaxPricePerLiterViewModel> GetMinMaxPricePerLiter(List<BeerViewModel.Beer> beers)
        {
            if (beers == null) throw new ArgumentException("beers was Null");
            try
            {
                // select every beer with max and min price 
                var query = beers.Select(
                    s => new
                    {
                        MaxUnitPrice = s.articles.Select(max => float.Parse(max.pricePerUnitText.Substring(1, (max.pricePerUnitText.IndexOf(" "))))).Max()
                    ,
                        MinUnitPrice = s.articles.Select(min => float.Parse(min.pricePerUnitText.Substring(1, (min.pricePerUnitText.IndexOf(" "))))).Min()
                    ,
                        beer = s
                    }
                    ).AsQueryable();
                var result = query.Select(s => new MinMaxPricePerLiterViewModel
                {
                    expensives = query
                    .Where(m => m.MaxUnitPrice == query.Max(m => m.MaxUnitPrice))
                    .Select(smax => 
                       new BeerViewModel.Beer { brandName = smax.beer.brandName,
                           descriptionText = smax.beer.descriptionText,
                           id = smax.beer.id,
                           name = smax.beer.name,
                           // filter articles : choose only valid articles that are MAX valu
                           articles = smax.beer.articles.Where(max => float.Parse(max.pricePerUnitText.Substring(1, (max.pricePerUnitText.IndexOf(" ")))) == query.Max(x => x.MaxUnitPrice)).ToArray() })
                    .ToList(),
                    cheaps = query
                    .Where(m => m.MinUnitPrice == query.Min(m => m.MinUnitPrice))
                    .Select(smin =>
                    new BeerViewModel.Beer { brandName = smin.beer.brandName,
                        descriptionText = smin.beer.descriptionText,
                        id = smin.beer.id,
                        name = smin.beer.name,
                        // filter articles : choose only valid articles that are min valu
                        articles = smin.beer.articles.Where(min => float.Parse(min.pricePerUnitText.Substring(1, (min.pricePerUnitText.IndexOf(" ")))) == query.Min(x => x.MinUnitPrice)).ToArray() })
                    .ToList()
                }) .FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        //******************************************************************************************************************
        /// <summary>
        /// It also has one route to get the answer to all routes or questions at once.
        /// </summary>
        #region GetAll

        public async Task<AllResultViewModel> GetAll(string url, float price)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<BeerViewModel.Beer>>(content);
                    var result = await GetAll(model, price);
                    return result;
                }
                throw new InvalidOperationException(response.Content.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<AllResultViewModel> GetAll(List<BeerViewModel.Beer> beers, float _price)
        {
            if (beers == null) throw new ArgumentException("beers was Null");
            try
            {
                AllResultViewModel result = new AllResultViewModel();
                result.MostBottlesList= await GetByMostBottles(beers);
                result.ExactPriceList = await GetByPrice(beers,_price);
                result.MinMaxPricePerLiter =await GetMinMaxPricePerLiter(beers);
                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion

        //******************************************************************************************************************
    }
}
