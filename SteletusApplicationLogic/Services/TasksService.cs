using System;
using System.Collections.Generic;
using System.Text;

namespace SteletusApplicationLogic.Services
{
    public class TasksService : SteletusApplicationLogic.Interfaces.ITasksService
    {
        private readonly SteletusApplicationLogic.IRepositories.ITasksRepository _tasksRepository;

        public TasksService(SteletusApplicationLogic.IRepositories.ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        public async Task<int> CreateTask(SteletusApplicationCore.Models.Task task)
        {
            var newTask = new SteletusApplicationCore.Models.Task(Guid.NewGuid(),task.Title,task.Description,task.AssignedTo,task.AssignedBy);
            int result = await _tasksRepository.CreateTask(newTask);
            return result;
        }

        public async Task<SteletusApplicationCore.Models.Task?> GetTaskById(Guid taskId)
        {
            var result = await _tasksRepository.GetTaskById(taskId);
            return  result;
        }

        public async Task<List<SteletusApplicationCore.Models.Task>> ListTasks(Guid? assignedToUserId)
        {
            var result = await _tasksRepository.ListTasks(assignedToUserId);
            return result;
        }

        public async Task<int> UpdateTask(SteletusApplicationCore.Models.Task task)
        {
            var result = await _tasksRepository.UpdateTask(task);
            return result;
        }
    }
}
