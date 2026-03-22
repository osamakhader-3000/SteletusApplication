using System;
using System.Collections.Generic;
using System.Text;

namespace SteletusApplicationDataAccess.Mappers
{
    public static class TasksMapper
    {
        public static SteletusApplicationCore.Models.Task ToCoreModel(this SteletusApplicationDataAccess.Models.Task task)
        {
            return new SteletusApplicationCore.Models.Task(task.Id, task.Title, task.Description ?? string.Empty, task.AssignedTo ?? Guid.Empty, task.AssignedBy ?? Guid.Empty);
        }


        public static SteletusApplicationDataAccess.Models.Task ToDataModel(this SteletusApplicationCore.Models.Task task)
        {
            return new SteletusApplicationDataAccess.Models.Task
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                AssignedTo = task.AssignedTo,
                AssignedBy = task.AssignedBy
            };
        }


        public static IEnumerable<SteletusApplicationCore.Models.Task> ToCoreModelList(this IEnumerable<SteletusApplicationDataAccess.Models.Task> tasks)
        {
            foreach (var task in tasks)
            {
                yield return task.ToCoreModel();
            }
        }


        public static IEnumerable<SteletusApplicationDataAccess.Models.Task> ToDataModelList(this IEnumerable<SteletusApplicationCore.Models.Task> tasks)
        {
            foreach (var task in tasks)
            {
                yield return task.ToDataModel();
            }
        }


    }
}
