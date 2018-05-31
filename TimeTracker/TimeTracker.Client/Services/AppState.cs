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
        public IList<Project> Projects { get; set; }
        public bool ActiveProjectsShown { get; set; } = true;
        public Project ActiveProject { get; private set; }
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


            await RetrieveProjects();
        }

        public void Logout()
        {
            User = null;
            Projects = null;
            NotifyStateChanged();
        }

        public async Task CreateProject(CreateProjectModel project)
        {
            await http.PostJsonAsync("api/project/add", project);
            await RetrieveProjects();
        }

        public async Task RetrieveProjects()
        {
            Projects = await http.GetJsonAsync<IList<Project>>("api/project/getprojects/" + User.Id);
            Projects = Projects.Where(p => p.Active == ActiveProjectsShown).ToList();
            NotifyStateChanged();
        }

        public async Task ShowOtherProjects()
        {
            ActiveProjectsShown = !ActiveProjectsShown;
            await RetrieveProjects();
        }

        public async Task ChangeProjectState(Project project)
        {
            await http.PostJsonAsync("api/project/modify", new ModifyProjectModel(project.Id, project.Name,project.Description, !project.Active));
            await RetrieveProjects();
        }

        public async Task SetActiveProject(Project project)
        {
            ActiveProject = project;
            ActiveProject.Activities = await http.GetJsonAsync<IList<Activity>>("api/activity/getall/" + ActiveProject.Id);
            NotifyStateChanged();
        }

        public async Task SaveActivity(ActivityModel activity)
        {
            await http.PostJsonAsync("api/activity/add", activity);
            ActiveProject.Activities = await http.GetJsonAsync<IList<Activity>>("api/activity/getall/" + ActiveProject.Id);
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange.Invoke();
    }
}
