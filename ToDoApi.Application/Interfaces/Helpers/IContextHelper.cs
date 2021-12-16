using ToDoApi.Domain.Entities;

namespace ToDoApi.Application.Interfaces.Helpers
{
    public interface IContextHelper
    {
        string GetUserName();
        void UpdateModel(ToDoItem toDoItem);
    }
}
