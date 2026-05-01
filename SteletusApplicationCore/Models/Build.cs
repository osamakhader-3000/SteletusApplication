using System;
using System.Collections.Generic;
using System.Text;

namespace SteletusApplicationCore.Models
{
    public class Build
    {
        public Guid Id { get; private set; }
        public string? Version { get; private set; }
        public string? Link { get; private set; }
        public IEnumerable<Client>? Clients { get; private set; }

        public IEnumerable<BuildLog>? BuildLogs { get; private set; }

        public Build(Guid id, string version, string? link, IEnumerable<Client>? clients, IEnumerable<BuildLog>? buildLogs)
        {
            Id = id;
            Version = version;
            Link = link;
            Clients = clients;
            BuildLogs = buildLogs;
        }
    }
}
