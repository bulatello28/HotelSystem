using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Classes
{
    public class HotelInfo
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public string CountryName { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public string Address { get; set; }

        public HotelInfo(int id, byte[] photo, string countryName, string name, int rate, string address)
        {
            Id = id;
            Photo = photo;
            CountryName = countryName;
            Name = name;
            Rate = rate;
            Address = address;
        }
    }
}
