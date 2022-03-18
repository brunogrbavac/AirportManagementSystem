using DomainModel.Models;
using FlightManagementBlazorServer.Services;
using FlightManagementBlazorServer.ValidationModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementBlazorServer.Pages
{
    public class EditCarrierBase : ComponentBase
    {

        [Parameter]
        public string CarrierId { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        [Inject]
        private CarrierService _carrierService { get; set; }

        [Inject]
        public StateService _stateService { get; set; }

        public Carrier Carrier { get; set; }
        public NotificationDialog NotificationDialog { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
        public String ConcatenatedValidationErrors { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Carrier = await _carrierService.GetCarrierAsync(int.Parse(CarrierId));
        }

        public void Close()
        {
            _navigationManager.NavigateTo("/CarrierList");
        }

        public async Task UpdateCarrierAsync()
        {
            ValidationErrors = ValidateCarrier();
            if (ValidationErrors.Any())
            {
                ConcatenatedValidationErrors = GetConcatenatedValidationErrors(ValidationErrors);
                NotificationDialog.Show();
            }
            else
            {
                await _carrierService.UpdateCarrierAsync(Carrier);
                Close();
            }
        }

        private List<ValidationError> ValidateCarrier()
        {
            var validationErrors = new List<ValidationError>();
            if (String.IsNullOrWhiteSpace(Carrier.Name))
                validationErrors.Add(new ValidationError { Description = "Please insert carrier name!" });

            if (String.IsNullOrWhiteSpace(Carrier.Country))
                validationErrors.Add(new ValidationError { Description = "Please insert Country!" });

            return validationErrors;

        }

        private string GetConcatenatedValidationErrors(List<ValidationError> ValidationErrors)
        {
            StringBuilder message = new StringBuilder();
            foreach (var error in ValidationErrors)
            {
                if (message.Length == 0)
                    message.Append(error.Description);
                else
                    message.Append($"{Environment.NewLine} {error.Description}");
            }
            return message.ToString();
        }
    }
}
