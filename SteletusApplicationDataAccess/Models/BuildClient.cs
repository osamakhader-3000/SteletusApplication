using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SteletusApplicationDataAccess.Models
{
    public class BuildClient
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        public Guid BuildId { get; set; }
        public string ClientName { get; set; } = string.Empty;

        public Build Build { get; set; } = new Build();
    }
}
