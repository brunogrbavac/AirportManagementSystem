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
    public class AddFlightBase : ComponentBase
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; }

        [Inject]
        private FlightService _flightService { get; set; }

        [Inject]
        public StateService _stateService { get; set; }

        public Flight Flight { get; set; }
        public NotificationDialog NotificationDialog { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
        public String ConcatenatedValidationErrors { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Flight = new Flight();
            Flight.FlightDate = DateTime.Now;
        }

        public async Task AddFlightAsync()
        {
            ValidationErrors = ValidateFlight();
            if (ValidationErrors.Any())
            {
                ConcatenatedValidationErrors = GetConcatenatedValidationErrors(ValidationErrors);
                NotificationDialog.Show();
            }
            else
            {
                await _flightService.AddFlightAsync(Flight);
                Close();
            }
        }

        public void Close()
        {
            _navigationManager.NavigateTo("/");
        }

        private List<ValidationError> ValidateFlight()
        {
            var validationErrors = new List<ValidationError>();
            if (String.IsNullOrWhiteSpace(Flight.Number))
                validationErrors.Add(new ValidationError { Description = "Please insert flight number!" });

            if (String.IsNullOrWhiteSpace(Flight.AirportTo))
                validationErrors.Add(new ValidationError { Description = "Please insert Airport To!" });

            if (Flight.CarrierId == null)
                validationErrors.Add(new ValidationError { Description = "Please select Carrier!" });

            if (String.IsNullOrWhiteSpace(Flight.FlightTime))
                validationErrors.Add(new ValidationError { Description = "Please insert Flight Time!" });

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
