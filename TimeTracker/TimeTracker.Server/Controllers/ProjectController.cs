using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Shared;
using TimeTracker.Shared.ApiMessages;
using TimeTracker.Shared.Model;

namespace TimeTracker.Server.Controllers
{

    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        private readonly ITrackerRepository _repository;

        public ProjectController(ITrackerRepository userRepository)
        {
            _repository = userRepository;
        }

        [HttpPost("[action]")]
        public void Add([FromBody] CreateProjectModel model)
        {
            var user = _repository.GetUser(model.User);
            var project = new Project(model.Name, model.Description);

            _repository.AddProject(project, user);

            return;
        }

        [HttpGet("[action]/{user}")]
        public IList<Project> GetProjects(Guid user)
        {
            return _repository.GetProjects(user);
        }

        [HttpPost("[action]")]
        public void Modify([FromBody]ModifyProjectModel model)
        {
            var project = _repository.GetProject(model.Id);
            project.Name = model.Name;
            project.Description = model.Description;
            project.Active = model.Active;
            _repository.ModifyProject(project);
        }

    }
}