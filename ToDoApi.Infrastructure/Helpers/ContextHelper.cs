using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using ToDoApi.Application.Interfaces.Helpers;
using ToDoApi.Domain.Entities;

namespace ToDoApi.Infrastructure.Helpers
{
    public class ContextHelper : IContextHelper
    {
        private readonly IHttpContextAccessor _accessor;
        public ContextHelper(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public string GetUserName()
        {
            return _accessor?.HttpContext.User?.Claims?.FirstOrDefault().Value;
        }

        public void UpdateModel(ToDoItem toDoItem)
        {
            toDoItem.LastUpdated = DateTime.UtcNow.ToString();
            toDoItem.UserName = GetUserName();
        }
    }
}
