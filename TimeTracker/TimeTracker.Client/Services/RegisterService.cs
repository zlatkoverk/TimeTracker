using Microsoft.AspNetCore.Blazor;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TimeTracker.Shared;
using TimeTracker.Shared.ApiMessages;
using TimeTracker.Shared.Model;

namespace TimeTracker.Client.Services
{
    public class RegisterService
    {
        private readonly HttpClient http;
        public event Action OnChange;
        public string Username { get; set; }
        public bool IsAvailable { get; private set; }
        public string UsernameMessage { get; private set; }
        public string GeneralMessage { get; set; }

        public RegisterService(HttpClient http)
        {
            this.http = http;
        }

        public async Task CheckAvailability()
        {
            var response = await http.GetJsonAsync<Response>("api/user/free/" + Username);
            IsAvailable = !response.Error;

            if (IsAvailable)
            {
                UsernameMessage = "Username is available";
            }
            else
            {
                UsernameMessage = "Username is not available";
            }

            NotifyStateChanged();
        }

        public async Task Register(RegisterModel model)
        {
            Username = model.Username;
            await CheckAvailability();
            if (!IsAvailable)
            {
                GeneralMessage = "Please choose another username";
                return;
            }

            GeneralMessage = "";
            UsernameMessage = "";
            await http.PostJsonAsync<UserCredentials>("api/user/register", model);
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
