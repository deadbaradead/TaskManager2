﻿using System;
using TaskManager.DataService.Database.DbModels;
using TaskManager.DataService.Models;

namespace TaskManager.DataService.Converters
{
    public class RecipientTaskConverter
    {
        public static RecipientTask Convert(Task t)
        {
            return new RecipientTask
            {
                Id = t.TaskId,
                Text = t.TaskText,
                // todo: remove checking null value
                AssignDateTime = t.AssignDateTime.HasValue ? t.AssignDateTime.Value : new DateTime(),
                Deadline = t.Deadline.HasValue ? t.Deadline.Value : new DateTime(),
                SenderId = t.SenderId,
                RecipientId = t.RecipientId,
                SenderName = t.TaskSender.UserFullName,
                CompleteDateTime = t.CompleteDate,
                AcceptCompleteDateTime = t.AcceptCpmpleteDate,
                PriorityId = t.TaskPriority != null ? t.TaskPriority.PriorityId : (int?)null,
                PriorityName = t.TaskPriority != null ? t.TaskPriority.PriorityName : null,
                IsRecipientViewed = t.IsRecipientViewed
            };
        }





    }
}