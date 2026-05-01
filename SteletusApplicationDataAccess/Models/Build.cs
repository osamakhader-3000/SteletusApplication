using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SteletusApplicationDataAccess.Models
{
    public class Build
    {
        [Key]
        public Guid Id { get; set; }
        public string? BuildVersion { get; set; }
        public string? Link { get; set; }
        public List<Client>? Clients { get; set; }
        public List<BuildLog>? BuildLogs { get; set; }

    }
}
