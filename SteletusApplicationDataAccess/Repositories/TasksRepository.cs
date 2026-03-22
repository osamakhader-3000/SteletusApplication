
using Microsoft.EntityFrameworkCore;
using SteletusApplicationDataAccess.Mappers;
using SteletusApplicationLogic.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteletusApplicationDataAccess.Repositories
{
    public class TasksRepository : ITasksRepository
    {

        private readonly Models.SteletusApplicationDataAccessContext _context;
        public TasksRepository(Models.SteletusApplicationDataAccessContext context)
        {
            _context = context;
        }

        public async Task<int> CreateTask(SteletusApplicationCore.Models.Task task)
        {
            _context.Tasks.Add(new Models.Task
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                AssignedTo = task.AssignedTo,
                AssignedBy = task.AssignedBy
            });

            int entriesNumber = await _context.SaveChangesAsync();

            return entriesNumber;
        }


        public async Task<SteletusApplicationCore.Models.Task?> GetTaskById(Guid taskId)
        {
            Models.Task? task = await _context.Tasks.FindAsync(taskId);

            if (task == null)
                return null;

            return task.ToCoreModel();
        }


        public async Task<List<SteletusApplicationCore.Models.Task>> ListTasks(Guid? assignToUserId)
        {
            List<Models.Task> tasks = await _context.Tasks.Where(t => assignToUserId == null || t.AssignedTo == assignToUserId).ToListAsync();
            return tasks.Select(t => t.ToCoreModel()).ToList();
        }


        public async Task<int> UpdateTask(SteletusApplicationCore.Models.Task task)
        {
            Models.Task? t = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == task.Id);

            if (t != null)
            {
                t.Title = task.Title;
                t.Description = task.Description;
                t.AssignedTo = task.AssignedTo;
                t.AssignedBy = task.AssignedBy;
            }

            int entriesNumber = await _context.SaveChangesAsync();
            return entriesNumber;
        }

    }
}
