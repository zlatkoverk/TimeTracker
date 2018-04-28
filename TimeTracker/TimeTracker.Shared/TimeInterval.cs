using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTracker.Shared
{
    public class TimeInterval
    {
        public Guid Id { get; set; }
        public Project Project { get; set; }
    }
}
