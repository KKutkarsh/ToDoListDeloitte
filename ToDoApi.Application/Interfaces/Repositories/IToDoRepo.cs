using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApi.Domain.Entities;

namespace ToDoApi.Application.Interfaces.Repositories
{
    public interface IToDoRepo
    {
        Task<IEnumerable<ToDoItem>> GetAllItems(string userName);
        Task<ToDoItem> GetItemsById(string userName, int id);
        Task<bool> Update(int id, ToDoItem toDoItem);
        Task Add(ToDoItem toDoItem);
        Task Delete(ToDoItem toDoItem);
    }
}
