using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TimeTracker.Shared;
using TimeTracker.Shared.Model;
using Microsoft.AspNetCore.Blazor;
using System.Linq;


namespace TimeTracker.Client.Services
{
    public class AppState
    {
        public IList<Project> ActiveProjects { get; set; }
        public event Action OnChange;
        private readonly HttpClient http;
        public bool LoggedIn { get => User != null; }
        public User User { get; private set; }
        public AppState(HttpClient http)
        {
            this.http = http;
        }


        public async Task Login(LoginModel model)
        {
            User = await http.PostJsonAsync<User>("api/user/login", model);

            NotifyStateChanged();
        }

        public void Logout()
        {
            User = null;
            NotifyStateChanged();
        }

        public async Task CreateProject(CreateProjectModel project)
        {
            await http.PostJsonAsync("api/project/add", project);
            await RetrieveActiveProjects(project.User);
        }

        public async Task RetrieveActiveProjects(Guid userId)
        {
            ActiveProjects = await http.GetJsonAsync<IList<Project>>("api/project/getprojects/" + userId);
            ActiveProjects = ActiveProjects.Where(p => p.Active).ToList();
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange.Invoke();
    }
}
