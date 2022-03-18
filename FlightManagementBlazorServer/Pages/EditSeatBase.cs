using DomainModel.Models;
using FlightManagementBlazorServer.Services;
using FlightManagementBlazorServer.ValidationModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementBlazorServer.Pages
{
    public class EditSeatBase : ComponentBase
    {
        [Parameter]
        public string SeatId { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        [Inject]
        private SeatService _seatService { get; set; }

        [Inject]
        public StateService _stateService { get; set; }

        public Seat Seat { get; set; }
        public NotificationDialog NotificationDialog { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
        public String ConcatenatedValidationErrors { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Seat = await _seatService.GetSeatAsync(int.Parse(SeatId));
        }

        public void Close()
        {
            _navigationManager.NavigateTo("/");
        }

        public async Task UpdateSeatAsync()
        {
            await _seatService.UpdateSeat(Seat);
            Close();
        }

        private async Task<List<ValidationError>> ValidateSeat()
        {
            var validationErrors = new List<ValidationError>();
            if (String.IsNullOrWhiteSpace(Seat.Number))
                validationErrors.Add(new ValidationError { Description = "Please insert seat number!" });

            if (Seat.FlightId == null)
                validationErrors.Add(new ValidationError { Description = "Please select Flight!" });

            if (Seat.PassengerId == null)
                validationErrors.Add(new ValidationError { Description = "Please select Passenger!" });


            var isUnique = await _seatService.UniqueSeat(Seat.Number, Seat.FlightId);
            if (!isUnique)
                validationErrors.Add(new ValidationError { Description = "That seat is already taken!" });

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
