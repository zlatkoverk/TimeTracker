using Microsoft.AspNetCore.Blazor;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TimeTracker.Shared;
using TimeTracker.Shared.ApiMessages;

namespace TimeTracker.Client.Services
{
    public class RegisterService
    {
        private readonly HttpClient http;
        public event Action OnChange;
        public bool IsAvailable { get; private set; }
        public string Message { get; private set; }

        public RegisterService(HttpClient http)
        {
            this.http = http;
            Message = "Enter the username";
        }

        public async Task CheckAvailability(string username)
        {
            var response = await http.GetJsonAsync<Response>("api/user/free/" + username);
            IsAvailable = !response.Error;

            if (IsAvailable)
            {
                Message = "Username is available";
            }
            else
            {
                Message = "Username is not available";
            }

            NotifyStateChanged();
        }

        public async Task Register(UserCredentials credentials)
        {
            await CheckAvailability(credentials.Username);
            if (!IsAvailable)
            {
                return;
            }

            await http.PostJsonAsync<UserCredentials>("api/user/register", credentials);
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
