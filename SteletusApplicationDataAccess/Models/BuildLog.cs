using System;
using System.Collections.Generic;
using System.Text;

namespace SteletusApplicationDataAccess.Models
{
    public class BuildLog
    {
        public Int64 Id { get; set; }

        public Build? Build { get; set; }

        public Nullable<Guid> BuildId { get; set; }
        public DateTime Timestamp { get; set; }

        public Guid UserId { get; set; }

    }
}
