using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime FlightDate { get; set; }
        public string FlightTime { get; set; }
        public string AirportTo { get; set; }

        public bool IsArchived { get; set; }

        public int? CarrierId { get; set; }
        public Carrier Carrier { get; set; }

    }
}
