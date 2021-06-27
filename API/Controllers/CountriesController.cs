using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Countries;

namespace API.Controllers
{
    public class CountriesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<CountryDto>>> GetCountries()
        {
            return await Mediator.Send(new Application.Countries.List.Query());
        }
    }
}