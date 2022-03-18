using DomainModel.Models;
using FlightManagementBlazorServer.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FlightManagementBlazorServer.Pages
{
    public class PassengerListBase : ComponentBase
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; }

        [Inject]
        private PassengerService _passengerService { get; set; }
        
        [Inject]
        public StateService _stateService { get; set; }

        public List<Passenger> Passengers { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Passengers = await GetPassengersAsync();
        }
        protected void ShowAddPassengerPage()
        {
            _navigationManager.NavigateTo("/AddPassenger");
        }

        protected void OpenEditPassengerPage(int passengerId)
        {
            _navigationManager.NavigateTo($"/EditPassenger/{passengerId}");
        }

        protected async Task DeletePassengerAsync(int passengerId)
        {
            await _passengerService.DeletePassengerAsync(passengerId);
            Passengers = await GetPassengersAsync();
        }

        protected async Task<List<Passenger>> GetPassengersAsync()
        {
            return await _passengerService.GetPassengersAsync();
        }
    }
}
