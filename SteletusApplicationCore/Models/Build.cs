using System;
using System.Collections.Generic;
using System.Text;

namespace SteletusApplicationCore.Models
{
    public class Build
    {
        public Build(Guid id, string version, string? link, List<string>? clients)
        {
            Id = id;
            Version = version;
            Link = link;
            Clients = clients;
        }

        public Guid Id { get; private set; }
        public string? Version { get; private set; }
        public string? Link { get; private set; }
        public List<string>? Clients { get; private set; }
    }
}
