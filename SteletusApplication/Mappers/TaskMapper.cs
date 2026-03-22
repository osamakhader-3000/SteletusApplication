namespace SteletusApplicationPresentation.Mappers
{
    public static class TaskMapper
    {
        public static SteletusApplicationPresentation.DataTransferObjects.Task ToDto(this SteletusApplicationCore.Models.Task task)
        {
            return new SteletusApplicationPresentation.DataTransferObjects.Task
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                AssignedTo = task.AssignedTo
            };
        }

        public static SteletusApplicationCore.Models.Task ToDomain(this SteletusApplicationPresentation.DataTransferObjects.Task taskDto)
        {
            return new SteletusApplicationCore.Models.Task(taskDto.Id ?? Guid.Empty, taskDto.Title, taskDto.Description, taskDto.AssignedTo ?? Guid.Empty, taskDto.AssignedBy ?? Guid.Empty);
        }

        public static IEnumerable<SteletusApplicationPresentation.DataTransferObjects.Task> ToDtoList(this List<SteletusApplicationCore.Models.Task> tasks)
        {
            return tasks.Select(t => t.ToDto());
        }

        public static IEnumerable<SteletusApplicationCore.Models.Task> ToDomainList(this IEnumerable<SteletusApplicationPresentation.DataTransferObjects.Task> taskDtos)
        {
            return taskDtos.Select(t => t.ToDomain());
        }

    }
}
