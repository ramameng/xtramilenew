using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Countries.Any()) return;

            var json = System.IO.File.ReadAllText("../Persistence/node_modules/all-countries-and-cities-json/countries.json");
            var result = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(json);
            var countries = new List<Country>();
            var cities = new List<City>();

            foreach(var country in result)
            {
                cities = new List<City>();
                foreach(var city in country.Value)
                {
                    cities.Add(new City
                    {
                        Name = city
                    });
                }
                await context.Cities.AddRangeAsync(cities);

                countries.Add(new Country
                {
                    Name = country.Key,
                    Cities = cities
                });
            }
            
            await context.Countries.AddRangeAsync(countries);
            await context.SaveChangesAsync();
        }
    }
}