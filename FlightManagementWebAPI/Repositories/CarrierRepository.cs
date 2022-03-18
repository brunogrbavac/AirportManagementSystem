using DomainModel.Models;
using FlightManagementWebAPI.DatabaseContext;
using System.Collections.Generic;
using System.Linq;

namespace FlightManagementWebAPI.Repositories
{
    public class CarrierRepository
    {
        private readonly AirportSystemContext _airportSystemContext;
        public CarrierRepository(AirportSystemContext airportSystemContext)
        {
            _airportSystemContext = airportSystemContext;
        }

        public List<Carrier> GetCarriers()
        {
            return _airportSystemContext.Carriers.ToList();
        }

        public void InsertCarrier(Carrier carrier)
        {
            _airportSystemContext.Carriers.Add(carrier);
            _airportSystemContext.SaveChanges();
        }

        public Carrier GetCarrier(int carrierId)
        {
            return _airportSystemContext.Carriers.FirstOrDefault(carrier => carrier.Id.Equals(carrierId));
        }

        public void UpdateCarrier(Carrier carrier)
        {
            var carrierForUpdate = GetCarrier(carrier.Id);
            if(carrierForUpdate != null)
            {
                carrierForUpdate.Name = carrier.Name;
                carrierForUpdate.Country = carrier.Country;

                _airportSystemContext.SaveChanges();
            }
        }

        public void DeleteCarrier(int carrierId)
        {
            var carrier = GetCarrier(carrierId);
            if(carrier != null)
            {
                _airportSystemContext.Carriers.Remove(carrier);
                _airportSystemContext.SaveChanges();
            }
        }
    }
}
