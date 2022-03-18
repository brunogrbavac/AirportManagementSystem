using System;

namespace FlightManagementBlazorServer.Services
{
    public class StateService
    {
        private string _loggedIn;
        public event Action OnChange;

        public string LoggedIn
        {
            get { return _loggedIn; }   
            set 
            { 
                if (_loggedIn != value)
                {
                    _loggedIn = value;
                    NotifyStateChanged();
                }
            }
        }

        private void NotifyStateChanged()
        {
            if(OnChange != null)
            {
                OnChange();
            }

        }
    }
}
