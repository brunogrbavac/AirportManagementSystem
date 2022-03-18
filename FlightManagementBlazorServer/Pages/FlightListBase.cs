using DomainModel.Models;
using FlightManagementBlazorServer.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightManagementBlazorServer.Pages
{
    public class FlightListBase : ComponentBase
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; }

        [Inject]
        private FlightService _flightService { get; set; }

        [Inject]
        public StateService _stateService { get; set; }

        public List<Flight> Flights;
        public ConfirmationDialog DeleteConfirmationDialog { get; set; }
        public ConfirmationDialog ArchiveConfirmationDialog { get; set; }
        public int SelectedFlightId { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Flights = await _flightService.GetFlights();
        }

        public void OpenAddSeatPage(int flightId)
        {
            _navigationManager.NavigateTo($"/AddSeat/{flightId}");
        }
        public void OpenEditFlightPage(int flightId)
        {
            _navigationManager.NavigateTo($"/EditFlight/{flightId}");
        }
        public void OpenSeatListPage(int flightId)
        {
            _navigationManager.NavigateTo($"/SeatList/{flightId}");
        }

        public void OpenAddFlightPage()
        {
            _navigationManager.NavigateTo("/AddFlight");
        }
        public void OpenCheckedSeatListPage(int flightId)
        {
            _navigationManager.NavigateTo($"/CheckedSeatsList/{flightId}");
        }

        public async Task DeleteFlight(int flightId)
        {
            SelectedFlightId = flightId;
            DeleteConfirmationDialog.Show();
        }

        public async Task OnDeleteConfirmationSelected(bool isDeleteConfirmed)
        {
            if (isDeleteConfirmed)
            {
                await _flightService.DeleteFlight(SelectedFlightId);
                Flights = await _flightService.GetFlights();
            }
        }

        public async Task OnArchiveConfirmationSelected(bool isArchiveConfirmed)
        {
            if (isArchiveConfirmed)
            {
                await _flightService.ArchiveFlight(SelectedFlightId);
                Flights = await _flightService.GetFlights();
            }
        }

        public async Task ArchiveFlight(int flightId)
        {
            SelectedFlightId = flightId;
            ArchiveConfirmationDialog.Show();
        }
    }
}
