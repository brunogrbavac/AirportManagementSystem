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
    public class AddUserBase : ComponentBase
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Inject]
        private UserService _userService { get; set; }

        [Inject]
        public StateService _stateService { get; set; }

        public User User { get; set; }
        public NotificationDialog NotificationDialog { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
        public String ConcatenatedValidationErrors { get; set; }

        protected override async Task OnInitializedAsync()
        {
            User = new User();
        }
        public void Close()
        {
            _navigationManager.NavigateTo("/UserList");
        }

        public async Task AddUserAsync()
        {
            ValidationErrors = ValidateUseer();
            if (ValidationErrors.Any())
            {
                ConcatenatedValidationErrors = GetConcatenatedValidationErrors(ValidationErrors);
                NotificationDialog.Show();
            }
            else
            {
                await _userService.AddUserAsync(User);
                Close();
            }
        }

        private List<ValidationError> ValidateUseer()
        {
            var validationErrors = new List<ValidationError>();
            if (String.IsNullOrWhiteSpace(User.Username))
                validationErrors.Add(new ValidationError { Description = "Please insert carrier username!" });

            if (String.IsNullOrWhiteSpace(User.Name))
                validationErrors.Add(new ValidationError { Description = "Please insert name and surname!" });

            if (String.IsNullOrWhiteSpace(User.Email))
                validationErrors.Add(new ValidationError { Description = "Please insert email!" });

            if (String.IsNullOrWhiteSpace(User.Role))
                validationErrors.Add(new ValidationError { Description = "Please insert role!" });

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
