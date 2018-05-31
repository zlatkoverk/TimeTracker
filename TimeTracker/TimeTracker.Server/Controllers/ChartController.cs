using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Shared;
using TimeTracker.Shared.ApiMessages;
using TimeTracker.Shared.Model;

namespace TimeTracker.Server.Controllers
{

    [Route("api/[controller]")]
    public class ChartController : Controller
    {
        private readonly ITrackerRepository _repository;

        public ChartController(ITrackerRepository userRepository)
        {
            _repository = userRepository;
        }

        [HttpGet("[action]/{projectId}")]
        public object[] ByMinutes(Guid projectId)
        {
            var project = _repository.GetProject(projectId);

            List<object> data = new List<object>();
            var header = new List<string>();
            header.Add("Duration");
            header.Add("Number of activities");
            data.Add(header);
            Dictionary<int, int> durations = new Dictionary<int, int>();

            foreach (var activity in project.Activities)
            {
                var duration = (int)activity.Duration.TotalMinutes;
                if (!durations.ContainsKey(duration))
                {
                    durations[duration] = 1;
                }
                else
                {
                    durations[duration]++;
                }
            }

            foreach (var duration in durations.Keys)
            {
                data.Add(new object[] { duration + " min", durations[duration] });
            }

            return data.ToArray();
        }

        [HttpGet("[action]/{projectId}")]
        public object[] ByLabels(Guid projectId)
        {
            var project = _repository.GetProject(projectId);

            List<object> data = new List<object>();
            var header = new List<string>();
            header.Add("Label");
            header.Add("Duration");
            data.Add(header);
            Dictionary<string, TimeSpan> durations = new Dictionary<string, TimeSpan>();

            foreach (var activity in project.Activities)
            {
                if (!durations.ContainsKey(activity.Label))
                {
                    durations[activity.Label] = activity.Duration;
                }
                else
                {
                    durations[activity.Label] = durations[activity.Label].Add(activity.Duration);
                }
            }

            foreach (var label in durations.Keys)
            {
                data.Add(new object[] { label, (int)durations[label].TotalMinutes });
            }

            return data.ToArray();
        }
    }
}