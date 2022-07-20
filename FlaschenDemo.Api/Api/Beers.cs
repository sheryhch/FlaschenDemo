using FlaschenDemo.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlaschenDemo.Api.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class Beers : ControllerBase
    {
        private readonly IBeerService _beerService;
        private readonly ILogger<Beers> _logger;
        public Beers(IBeerService beerService, ILogger<Beers> logger)
        {
            _beerService = beerService;
            _logger = logger;
        }
        /// <summary>
        /// Most expensive and cheapest beer per litre
        /// </summary>       
        [HttpGet("GetMinMaxPricePerLiter")]
        public async Task<IActionResult> GetMinMaxPricePerLiter([FromQuery] string url)
        {


            try
            {
                var result = await _beerService.GetMinMaxPricePerLiter(url);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Which beers cost exactly €17.99?
        /// </summary>
        [HttpGet("GetByPrice")]
        public async Task<IActionResult> GetByPrice([FromQuery] string url, [FromQuery] float price)
        {
            try
            {
                var result =await _beerService.GetByPrice(url, price);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Which one product comes in the most bottles?
        /// </summary>
        [HttpGet("GetByMostBottles")]
        public async Task<IActionResult> GetByMostBottles([FromQuery] string url)
        {
            try
            {
                var result =await _beerService.GetByMostBottles(url);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// It also has one route to get the answer to all routes or questions at once.
        /// </summary>      
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] string url, [FromQuery] float price)
        {
            try
            {
                var result = await _beerService.GetAll(url, price);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return BadRequest(ex.Message);
            }

        }

    }
}
