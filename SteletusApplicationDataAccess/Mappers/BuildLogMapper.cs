using System;
using System.Collections.Generic;
using System.Text;

namespace SteletusApplicationDataAccess.Mappers
{
    public static class BuildLogMapper
    {
        public static SteletusApplicationCore.Models.BuildLog ToCoreModel(this Models.BuildLog buildLog)
        {
            return new SteletusApplicationCore.Models.BuildLog(buildLog.Id, buildLog.BuildId ?? Guid.Empty, buildLog.Timestamp, buildLog.UserId);
        }

        public static Models.BuildLog ToDataModel(this SteletusApplicationCore.Models.BuildLog buildLog)
        {
            return new Models.BuildLog
            {
                Id = buildLog.Id,
                BuildId = buildLog.BuildId,
                Timestamp = buildLog.Timestamp,
                UserId = buildLog.UserId
            };
        }

        public static IEnumerable<SteletusApplicationCore.Models.BuildLog> ToCoreModels(this IEnumerable<Models.BuildLog> buildLogs)
        {
            foreach (var buildLog in buildLogs)
            {
                yield return buildLog.ToCoreModel();
            }
        }

        public static IEnumerable<Models.BuildLog> ToDataModels(this IEnumerable<SteletusApplicationCore.Models.BuildLog> buildLogs)
        {
            foreach (var buildLog in buildLogs)
            {
                yield return buildLog.ToDataModel();
            }
        }

    }
}
