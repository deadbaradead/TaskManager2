﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TaskManager.DataService.Converters;
using TaskManager.DataService.Database;
using TaskManager.DataService.Models;

namespace TaskManager.DataService.Services
{
    public class TaskService : ITaskService
    {
        public IEnumerable<RecipientTask> GetRecipientTasks()
        {
            using (var context = new TaskManagerContext())
            {
                return context.Tasks
                    .Include(x => x.TaskSender)
                    .Include(x => x.TaskRecipient)
                    .Include(x => x.TaskPriority)
                    .Select(RecipientTaskConverter.Convert).ToList();
            }
        }

        public RecipientTask GetRecipientTask(int taskId)
        {
            using (var context = new TaskManagerContext())
            {
                var task = context.Tasks
                    .Include(x => x.TaskSender)
                    .Include(x => x.TaskRecipient)
                    .Include(x => x.TaskPriority)
                    .FirstOrDefault(x => x.TaskId == taskId);
                if (task != null)
                {
                    return RecipientTaskConverter.Convert(task);
                }
                return null;
            }
        }

        public IEnumerable<SenderTask> GetSenderTasks(int senderId)
        {
            using (var context = new TaskManagerContext())
            {
                return context.Tasks
                    .Include(x => x.TaskSender)
                    .Include(x => x.TaskRecipient)
                    .Include(x => x.TaskPriority)
                    .Where(x => x.SenderId == senderId)
                    .Select(SenderTaskConverter.Convert);

            }
        }
    }
}