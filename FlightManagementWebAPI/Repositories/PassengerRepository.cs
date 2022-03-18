using DomainModel.Models;
using FlightManagementWebAPI.DatabaseContext;
using System.Collections.Generic;
using System.Linq;

namespace FlightManagementWebAPI.Repositories
{
    public class PassengerRepository
    {
        private readonly AirportSystemContext _airportSystemContext;
        public PassengerRepository(AirportSystemContext airportSystemContext)
        {
            _airportSystemContext = airportSystemContext;
        }

        public List<Passenger> GetPassengers()
        {
            return _airportSystemContext.Passengers.ToList();
        }

        public void InsertPassenger(Passenger passenger)
        {
            _airportSystemContext.Passengers.Add(passenger);
            _airportSystemContext.SaveChanges();
        }

        public Passenger GetPassenger(int passengerId)
        {
            return _airportSystemContext.Passengers.FirstOrDefault(passenger => passenger.Id.Equals(passengerId));
        }

        public void UpdatePassenger(Passenger passenger)
        {
            var passengerForUpdate = GetPassenger(passenger.Id);
            if (passengerForUpdate != null)
            {
                passengerForUpdate.Name = passenger.Name;
                passengerForUpdate.Surname = passenger.Surname;
                passengerForUpdate.Email = passenger.Email;
                passengerForUpdate.Gender = passenger.Gender;

                _airportSystemContext.SaveChanges();
            }
        }

        public void DeletePassenger(int passengerId)
        {
            var passenger = GetPassenger(passengerId);
            if (passenger != null)
            {
                _airportSystemContext.Passengers.Remove(passenger);
                _airportSystemContext.SaveChanges();
            }
        }
    }
}
