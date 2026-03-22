using System;
using System.Collections.Generic;
using System.Text;

namespace SteletusApplicationCore.Models
{
    public class Task
    {
        public Task(Guid id, string title, string description,Guid assignedTo,Guid assignedBy)
        {
            Id = id;
            Title = title;
            Description = description;
            AssignedTo = assignedTo;
            AssignedBy = assignedBy;
        }

        public Guid Id { get; private set; }

        public string Title { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;
        public Guid AssignedTo { get; private set; }
        public Guid AssignedBy { get; private set; }
    }
}
