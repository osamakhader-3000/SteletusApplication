using System;
using System.Collections.Generic;
using System.Text;

namespace SteletusApplicationLogic.IRepositories
{
    public interface ITasksRepository
    {
        Task<List<SteletusApplicationCore.Models.Task>> ListTasks(Guid? userId);

        Task<int> CreateTask(SteletusApplicationCore.Models.Task task);

        Task<SteletusApplicationCore.Models.Task?> GetTaskById(Guid taskId);

        Task<int> UpdateTask(SteletusApplicationCore.Models.Task task);
    }
}
