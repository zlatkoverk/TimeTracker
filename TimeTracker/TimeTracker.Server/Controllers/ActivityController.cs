using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Shared;
using TimeTracker.Shared.ApiMessages;
using TimeTracker.Shared.Model;

namespace TimeTracker.Server.Controllers
{

    [Route("api/[controller]")]
    public class ActivityController : Controller
    {
        private readonly ITrackerRepository _repository;

        public ActivityController(ITrackerRepository userRepository)
        {
            _repository = userRepository;
        }

        [HttpPost("[action]")]
        public void Add([FromBody] ActivityModel model)
        {
            if (model.ActivityId == null)
            {
                _repository.AddActivity(model.ProjectId, new Activity(model.Description, model.Duration, model.Label));
            }
            else
            {
                _repository.UpdateActivity(new Activity(model.Description, model.Duration, model.Label)
                {
                    Id = model.ActivityId.Value
                });
            }
        }

        [HttpGet("[action]/{project}")]
        public IList<Activity> GetAll(Guid project)
        {
            return _repository.GetActivities(project);
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