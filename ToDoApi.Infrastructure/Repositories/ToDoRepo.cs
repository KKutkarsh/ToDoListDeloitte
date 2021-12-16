using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Application.Interfaces.Repositories;
using ToDoApi.Domain.Entities;
using ToDoApi.Persistence.Context;

namespace ToDoApi.Infrastructure.Repositories
{
    public class ToDoRepo: IToDoRepo
    {
        private readonly ApplicationDbContext _context;
        public ToDoRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToDoItem>> GetAllItems( string userName)
        {
           return  await _context.ToDoItems.Where(x=>x.UserName== userName).ToListAsync();
        }

        public async Task<ToDoItem> GetItemsById(string userName, int id)
        {
            return await _context.ToDoItems.Where(x => x.UserName == userName && x.ItemId == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(int id, ToDoItem toDoItem)
        {
            _context.Entry(toDoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoItemModelExists(id))
                {
                    return false;
                }
                else
                {
                    throw new DbUpdateConcurrencyException($"Error Updated{ id}");
                }
            }
        }

        public async Task Add(ToDoItem toDoItem)
        {
            _context.ToDoItems.Add(toDoItem);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(ToDoItem toDoItem)
        {
            _context.ToDoItems.Remove(toDoItem);
            await _context.SaveChangesAsync();
        }

        private bool ToDoItemModelExists(int id)
        {
            return _context.ToDoItems.Any(e => e.ItemId == id);
        }
    }
}
