using Application.Cities;
using Application.Countries;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Country, CountryDto>();
            CreateMap<City, CityDto>();
        }
    }
}