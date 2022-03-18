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
    public class LoginBase : ComponentBase
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; }

        [Inject]
        private UserService _userService { get; set; }

        [Inject]
        public StateService _stateService { get; set; }

        public User User { get; set; }
        public NotificationDialog NotificationDialog { get; set; }
        public String ConcatenatedValidationErrors { get; set; }

        protected override async Task OnInitializedAsync()
        {
            User = new User();
        }

        public void Close()
        {
            _navigationManager.NavigateTo("/");
        }

        public async Task LoginUserAsync()
        {
            if (User.Username != null && User.Password !=null)
            {
                User user = await _userService.LoginAsync(User.Username, User.Password);
                _stateService.LoggedIn = user.Role;
                if (user.Role == "Admin" || user.Role=="CheckIn")
                {
                    Close();
                }
                else
                {
                    ConcatenatedValidationErrors = "Wrong username or password!";
                    NotificationDialog.Show();
                }
            }
        }
    }
}
