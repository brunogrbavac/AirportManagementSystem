using DomainModel.Models;
using FlightManagementWebAPI.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FlightManagementWebAPI.Repositories
{
    public class FlightRepository
    {
        private readonly AirportSystemContext _airportSystemContext;
        public FlightRepository(AirportSystemContext airportSystemContext)
        {
            _airportSystemContext = airportSystemContext;
        }

        public List<Flight> GetFlights(bool archived)
        {
            return _airportSystemContext.Flights.Include(flight => flight.Carrier).Where(flight => flight.IsArchived == archived).ToList();
        }

        public void InsertFlight(Flight flight)
        {
            _airportSystemContext.Flights.Add(flight);
            _airportSystemContext.SaveChanges();
        }

        public Flight GetFlight(int flightId)
        {
            return _airportSystemContext.Flights
                .FirstOrDefault(flight => flight.Id == flightId);
        }

        public void UpdateFlight(Flight flight)
        {
            var flightForUpdate = GetFlight(flight.Id);
            if(flightForUpdate != null)
            {
                flightForUpdate.Number = flight.Number;
                flightForUpdate.AirportTo = flight.AirportTo;
                flightForUpdate.FlightDate = flight.FlightDate;
                flightForUpdate.FlightTime = flight.FlightTime;
                flightForUpdate.CarrierId = flight.CarrierId;

                _airportSystemContext.SaveChanges();
            }
        }

        public void DeleteFlight(int flightId)
        {
            var flightForDelete = GetFlight(flightId);
            if(flightForDelete != null)
            {
                _airportSystemContext.Flights.Remove(flightForDelete);
                _airportSystemContext.SaveChanges();
            }
        }

        public void ArchiveFlight(int flightId)
        {
            var flight = GetFlight(flightId);
            if(flight != null)
            {
                flight.IsArchived = true;
                _airportSystemContext.SaveChanges();
            }
        }
    }
}
