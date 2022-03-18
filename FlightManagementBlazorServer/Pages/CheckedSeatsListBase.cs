using DomainModel.Models;
using FlightManagementBlazorServer.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightManagementBlazorServer.Pages
{
    public class CheckedSeatsListBase : ComponentBase
    {
        [Parameter]
        public string FlightId { get; set; }

        [Inject]
        private SeatService _seatService { get; set; }

        [Inject]
        public StateService _stateService { get; set; }

        public List<Seat> Seats;

        public int SelectedSeatId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Seats = await _seatService.GetCheckedSeats(int.Parse(FlightId));
        }

       
    }
}
