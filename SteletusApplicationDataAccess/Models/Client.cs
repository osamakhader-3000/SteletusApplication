using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SteletusApplicationDataAccess.Models
{
    public class Client
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } 

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? CurrentChangeRequest { get; set; }
        public string? LastChangeRequest { get; set; }

        public List<Build>? Builds { get; set; }

    }
}
