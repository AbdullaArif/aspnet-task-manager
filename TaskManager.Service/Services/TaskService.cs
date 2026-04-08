using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;

namespace TaskManager.Service.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetSortedAsync(string sortBy)
        {
            var tasks = await _repository.GetAllAsync();

            return sortBy switch
            {
                "priority_asc" => tasks.OrderBy(t => t.Priority),
                "priority_desc" => tasks.OrderByDescending(t => t.Priority),
                "deadline_asc" => tasks.OrderBy(t => t.Deadline),
                "deadline_desc" => tasks.OrderByDescending(t => t.Deadline),
                _ => tasks
            };
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(TaskItem task)
        {
            await _repository.AddAsync(task);
        }

        public async Task UpdateAsync(TaskItem task)
        {
            await _repository.UpdateAsync(task);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
