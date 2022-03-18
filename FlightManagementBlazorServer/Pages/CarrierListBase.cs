using DomainModel.Models;
using FlightManagementBlazorServer.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FlightManagementBlazorServer.Pages
{
    public class CarrierListBase : ComponentBase
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Inject]
        private CarrierService _carrierService { get; set; }
        [Inject]
        public StateService _stateService { get; set; }
        public List<Carrier> Carriers { get; set; }

        public ConfirmationDialog DeleteConfirmationDialog { get; set; }
        public int SelectedCarrierId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Carriers = await GetCarriersAsync();
        }
        protected void ShowAddCarrierPage()
        {
            _navigationManager.NavigateTo("/AddCarrier");
        }

        public async Task OpenDeleteCarrierPopup(int carrierId)
        {
            SelectedCarrierId = carrierId;
            DeleteConfirmationDialog.Show();
        }

        protected void OpenEditCarrierPage(int carrierId)
        {
            _navigationManager.NavigateTo($"/EditCarrier/{carrierId}");
        }

        protected async Task<List<Carrier>> GetCarriersAsync()
        {
            return await _carrierService.GetCarriersAsync();
        }
        public async Task OnDeleteConfirmationSelected(bool isDeleteConfirmed)
        {
            if (isDeleteConfirmed)
            {
                await _carrierService.DeleteCarrierAsync(SelectedCarrierId);
                Carriers = await GetCarriersAsync();
            }
        }

    }
}
