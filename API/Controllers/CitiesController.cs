using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Cities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CitiesController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<List<CityDto>>> GetCities(Guid id)
        {
            return await Mediator.Send(new Application.Cities.List.Query{id = id});
        }
    }
}