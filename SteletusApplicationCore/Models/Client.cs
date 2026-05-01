using System;
using System.Collections.Generic;
using System.Text;

namespace SteletusApplicationCore.Models
{
    public class Client
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string? CurrentChangeRequest { get; private set; }

        public string? LastChangeRequest { get; private set; }
        public string? Description { get; private set; }

        public IEnumerable<Build>? Builds { get; private set; } = null;

        public Client(Guid id, string name, string? description = null, string? currentChangeRequest = null, string? lastChangeRequest = null, IEnumerable<Build>? builds = null)
        {
            Id = id;
            Name = name;
            Description = description;
            CurrentChangeRequest = currentChangeRequest;
            LastChangeRequest = lastChangeRequest;
            Builds = builds;
        }

    }
}
