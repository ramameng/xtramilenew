using System;
using System.Collections.Generic;

namespace Domain
{
    public class Country
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}