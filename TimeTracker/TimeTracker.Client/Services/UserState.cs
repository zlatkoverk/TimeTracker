using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;
using TimeTracker.Shared;
using TimeTracker.Shared.ApiMessages;
using TimeTracker.Shared.Model;

namespace TimeTracker.Client.Services
{
    public class UserState
    {
        private readonly HttpClient http;
        public bool LoggedIn { get => User != null; }
        public User User { get; private set; }
        public event Action OnChange;

        public UserState(HttpClient http)
        {
            this.http = http;
        }

        public async Task Login(LoginModel model)
        {
            var response = await http.PostJsonAsync<Response>("api/user/exists", model);
            if (response.Error)
            {
                return;
            }

            User = await http.PostJsonAsync<User>("api/user/login", model);
            NotifyStateChanged();
        }

        public void Logout()
        {
            User = null;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
