using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Priority Priority { get; set; }
        public DateTime? Deadline { get; set; }
        public bool IsCompleted { get; set; }

        public string GetStatus()
        {
            if (IsCompleted) return "Done";
            if (Deadline.HasValue && Deadline.Value < DateTime.Now) return "Overdue";
            if (Deadline.HasValue && Deadline.Value <= DateTime.Now.AddHours(24)) return "Urgent";
            return "Active";
        }
    }

    public enum Priority
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
}
