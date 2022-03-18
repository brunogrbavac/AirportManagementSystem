using DomainModel.Models;
using FlightManagementBlazorServer.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FlightManagementBlazorServer.Pages
{
    public class UserListBase : ComponentBase
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Inject]
        private UserService _userService { get; set; }

        [Inject]
        public StateService _stateService { get; set; }

        public List<User> Users { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Users = await GetUsersAsync();
        }
        protected void ShowAddUserPage()
        {
            _navigationManager.NavigateTo("/AddUser");
        }

        protected void OpenEditUserPage(int userId)
        {
            _navigationManager.NavigateTo($"/EditUser/{userId}");
        }

        protected async Task DeleteUserAsync(int userId)
        {
            await _userService.DeleteUserAsync(userId);
            Users = await GetUsersAsync();
        }

        protected async Task<List<User>> GetUsersAsync()
        {
            return await _userService.GetUsersAsync();
        }
    }
}
