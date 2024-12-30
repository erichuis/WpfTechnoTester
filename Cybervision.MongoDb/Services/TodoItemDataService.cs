﻿using Cybervision.Dapr.DataModels;
using Cybervision.Dapr.Repositories;
using Domain.DataTransferObjects;

namespace Cybervision.Dapr.Services
{
    public class TodoItemDataService : BaseDataService<TodoItemDto, TodoItemRepository, TodoItemDocument>, ITodoItemDataService
    {
        public TodoItemDataService(TodoItemRepository repository):base(repository)
        {

        }
        
    }
}