using DomainModel.Models;
using FlightManagementBlazorServer.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightManagementBlazorServer.Pages
{
    public class SeatListBase : ComponentBase
    {
        [Parameter]
        public string FlightId { get; set; }

        [Inject]
        private SeatService _seatService { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        [Inject]
        public StateService _stateService { get; set; }

        public List<Seat> Seats;
        public ConfirmationDialog DeleteConfirmationDialog { get; set; }
        public ConfirmationDialog CheckConfirmationDialog { get; set; }
        public int SelectedSeatId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Seats = await _seatService.GetFlightSeats(int.Parse(FlightId));
        }

        public async Task DeleteSeat(int seatId)
        {
            SelectedSeatId = seatId;
            DeleteConfirmationDialog.Show();
        }

        public void OpenEditSeatPage(int seatId)
        {
            _navigationManager.NavigateTo($"/EditSeat/{seatId}");
        }

        public async Task OnDeleteConfirmationSelected(bool isDeleteConfirmed)
        {
            if (isDeleteConfirmed)
            {
                await _seatService.DeleteSeat(SelectedSeatId);
                Seats = await _seatService.GetSeats();
            }
        }

        public async Task OnCheckConfirmationSelected(bool isCheckConfirmed)
        {
            if (isCheckConfirmed)
            {
                await _seatService.CheckSeat(SelectedSeatId);
                Seats = await _seatService.GetSeats();
            }
        }

        public async Task CheckSeat(int seatId)
        {
            SelectedSeatId = seatId;
            CheckConfirmationDialog.Show();
        }
    }
}
