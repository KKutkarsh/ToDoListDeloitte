using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Application.Interfaces.Helpers;
using ToDoApi.Application.Interfaces.Repositories;
using ToDoApi.Domain.Entities;

namespace ToDoAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly IToDoRepo _toDoRepo;
        private readonly IContextHelper _context;

        public ToDoItemController(IToDoRepo toDoRepo, IContextHelper context)
        {
            _toDoRepo = toDoRepo;
            _context = context;
        }

        // GET: api/ToDoItem
        [HttpGet]
        public async Task<IEnumerable<ToDoItem>> GetToDoItems()
        {

            var userName = _context.GetUserName();
            return await _toDoRepo.GetAllItems(userName);
        }

        // GET: api/ToDoItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItemModel(int id)
        {
            var userName = _context.GetUserName();
            var toDoItemModel = await _toDoRepo.GetItemsById(userName, id);
            if (toDoItemModel == null)
            {
                return NotFound();
            }

            return toDoItemModel;
        }

        // PUT: api/ToDoItem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoItemModel(int id, ToDoItem toDoItemModel)
        {
            if (id != toDoItemModel.ItemId)
            {
                return BadRequest();
            }

            _context.UpdateModel(toDoItemModel);

            var isSusccess = await _toDoRepo.Update(id, toDoItemModel);
            if (!isSusccess)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/ToDoItem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> PostToDoItemModel(ToDoItem toDoItemModel)
        {

            _context.UpdateModel(toDoItemModel);
            await _toDoRepo.Add(toDoItemModel);

            return CreatedAtAction("GetToDoItemModel", new { id = toDoItemModel.ItemId }, toDoItemModel);
        }

        // DELETE: api/ToDoItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItemModel(int id)
        {
            var userName = _context.GetUserName();
            var toDoItemModel = await _toDoRepo.GetItemsById(userName, id);
            if (toDoItemModel == null)
            {
                return NotFound();
            }
            await _toDoRepo.Delete(toDoItemModel);

            return NoContent();
        }
    }
}