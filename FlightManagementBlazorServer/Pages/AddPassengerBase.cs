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
    public class AddPassengerBase : ComponentBase
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; }
       
        [Inject]
        private PassengerService _passengerService { get; set; }

        [Inject]
        public StateService _stateService { get; set; }

        public Passenger Passenger { get; set; }
        public NotificationDialog NotificationDialog { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
        public String ConcatenatedValidationErrors { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Passenger = new Passenger();
        }
        public void Close()
        {
            _navigationManager.NavigateTo("/PassengerList");
        }

        public async Task AddPassengerAsync()
        {
            ValidationErrors = ValidatePassenger();
            if (ValidationErrors.Any())
            {
                ConcatenatedValidationErrors = GetConcatenatedValidationErrors(ValidationErrors);
                NotificationDialog.Show();
            }
            else
            {
                await _passengerService.AddPassengerAsync(Passenger);
                Close();
            }
        }
        private List<ValidationError> ValidatePassenger()
        {
            var validationErrors = new List<ValidationError>();
            if (String.IsNullOrWhiteSpace(Passenger.Name))
                validationErrors.Add(new ValidationError { Description = "Please insert carrier name!" });

            if (String.IsNullOrWhiteSpace(Passenger.Surname))
                validationErrors.Add(new ValidationError { Description = "Please insert surname!" });

            if (String.IsNullOrWhiteSpace(Passenger.Gender))
                validationErrors.Add(new ValidationError { Description = "Please insert gender!" });

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
