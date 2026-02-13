using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Abstractions.VO
{
    public sealed class Location
    {
        public string Country { get; }
        public string City { get; }
        public string Street { get; }
        public string ZipCode { get; }

        public Location(string country, string city, string street, string zipCode)
        {
            Country = country;
            City = city;
            Street = street;
            ZipCode = zipCode;
        }
    }
}
