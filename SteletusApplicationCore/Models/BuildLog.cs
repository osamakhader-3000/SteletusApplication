using System;
using System.Collections.Generic;
using System.Text;

namespace SteletusApplicationCore.Models
{
    public class BuildLog
    {
        public Int64 Id { get; private set; }
        public Guid BuildId { get; private set; }
        public DateTime Timestamp { get; private set; }

        public Guid UserId { get; private set; }

        public BuildLog(Int64 id, Guid buildId, DateTime timestamp, Guid userId)
        {
            Id = id;
            BuildId = buildId;
            Timestamp = timestamp;
            UserId = userId;
        }
    }
}
