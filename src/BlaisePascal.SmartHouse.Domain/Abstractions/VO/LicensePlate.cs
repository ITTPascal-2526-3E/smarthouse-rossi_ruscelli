using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Abstractions.VO
{
    public sealed class LicensePlate
    {
        public string Number { get; }
        public LicensePlate(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                throw new ArgumentException("License plate number cannot be null or empty.", nameof(number));
            }           
            Number = number;
        }
    }
}
