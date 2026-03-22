using System;
using System.Collections.Generic;
using System.Text;

namespace SteletusApplicationDataAccess.Models
{
    public class Task
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }
        public Guid? AssignedTo { get; set; }
        public Guid? AssignedBy { get; set; }
    }
}
