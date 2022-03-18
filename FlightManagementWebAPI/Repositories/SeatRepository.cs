using DomainModel.Models;
using FlightManagementWebAPI.DatabaseContext;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementWebAPI.Repositories
{
    public class SeatRepository
    {
        private readonly AirportSystemContext _airportSystemContext;
        public SeatRepository(AirportSystemContext airportSystemContext)
        {
            _airportSystemContext = airportSystemContext;
        }

        public List<Seat> GetSeats(bool isChecked)
        {
            return _airportSystemContext.Seats.Include(seat => seat.Flight).Include(seat => seat.Passenger).ToList();
        }

        public List<Seat> GetFlightSeats(int flightId)
        {
            return _airportSystemContext.Seats.Include(seat => seat.Flight).Include(seat => seat.Passenger).Where(seat => seat.FlightId == flightId).ToList();
        }
        public List<Seat> GetCheckedFlightSeats(int flightId)
        {
            return _airportSystemContext.Seats.Include(seat => seat.Flight).Include(seat => seat.Passenger).Where(seat => (seat.FlightId == flightId) && (seat.IsChecked == true)).ToList();
        }

        public void InsertSeat(Seat seat)
        {
            _airportSystemContext.Seats.Add(seat);
            _airportSystemContext.SaveChanges();
        }

        public Seat GetSeat(int seatId)
        {
            return _airportSystemContext.Seats
                .FirstOrDefault(seat => seat.Id == seatId);
        }

        public void UpdateSeat(Seat seat)
        {
            var seatForUpdate = GetSeat(seat.Id);
            if (seatForUpdate != null)
            {
                seatForUpdate.Number = seat.Number;
                seatForUpdate.FlightId = seat.FlightId;
                seatForUpdate.PassengerId = seat.PassengerId;

                _airportSystemContext.SaveChanges();
            }
        }

        public void DeleteSeat(int seatId)
        {
            var seatForDelete = GetSeat(seatId);
            if (seatForDelete != null)
            {
                _airportSystemContext.Seats.Remove(seatForDelete);
                _airportSystemContext.SaveChanges();
            }
        }

        public void CheckSeat(int seatId)
        {
            var seat = GetSeat(seatId);
            if (seat != null)
            {
                seat.IsChecked = true;
                _airportSystemContext.SaveChanges();
            }
        }

        public bool UniqueSeatNumber(string seatNumber, int flightId)
        {
            var tempSeat = _airportSystemContext.Seats.FirstOrDefault(seat => (seat.Number == seatNumber && seat.FlightId == flightId));

            if (tempSeat != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
