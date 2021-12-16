using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApi.Application.Interfaces.Helpers;
using ToDoApi.Application.Interfaces.Repositories;
using ToDoApi.Domain.Entities;
using ToDoAPI.Controllers;

namespace ToDoApi.Tests.Controllers
{
    [TestFixture]
    public class ToDoItemControllerTests
    {
        private  IToDoRepo _toDoRepo;
        private IContextHelper _context;
        private ToDoItemController _sut;

        [SetUp]
        public void Setup()
        {
            _toDoRepo = Substitute.For<IToDoRepo>();
            _context = Substitute.For<IContextHelper>();
            _sut = new ToDoItemController(_toDoRepo, _context);

        }

        [Test]
        public async Task GivenThatUserIsAuthorized_whenGetToDoItemsIsCalled_ItHitsGetAllItems()
        {
            // Arrange
            var toDoItemList = new List<ToDoItem>();
            _context.GetUserName().Returns("username");
            _toDoRepo.GetAllItems(Arg.Any<string>()).Returns(toDoItemList);

            // Act
            await _sut.GetToDoItems();

            // Assert
            await _toDoRepo.Received(1).GetAllItems(Arg.Any<string>());
        }

        [Test]
        public async Task GivenThatUserIsAuthorized_whenGetToDoItemsIsCalled_ItHitsGetUserName()
        {
            // Arrange
            var toDoItemList = new List<ToDoItem>();
            _context.GetUserName().Returns("username");
            _toDoRepo.GetAllItems(Arg.Any<string>()).Returns(toDoItemList);

            // Act
            await _sut.GetToDoItems();

            // Assert
            _context.Received(1).GetUserName();
        }

        [Test]
        public async Task GivenThatUserIsAuthorized_whenGetToDoItemModelIsCalled_ItHitsGetItemsById()
        {
            // Arrange
            var toDoItem = GetToDoItem();
            _context.GetUserName().Returns("username");
            _toDoRepo.GetItemsById(Arg.Any<string>(),Arg.Any<int>()).Returns(toDoItem);

            // Act
            var v= await _sut.GetToDoItemModel(Arg.Any<int>());

            // Assert
            await _toDoRepo.Received(0).GetItemsById(Arg.Any<string>(), Arg.Any<int>());
        }

        [Test]
        public async Task GivenThatUserIsAuthorized_whenPutToDoItemModelIsCalled_ItHitsUpdate()
        {
            // Arrange
            var toDoItem = GetToDoItem();
            _context.Received(0).UpdateModel(toDoItem);
            _toDoRepo.Update(toDoItem.ItemId, toDoItem).Returns(true);
            // Act
            await _sut.PutToDoItemModel(toDoItem.ItemId, toDoItem);

            // Assert
            await _toDoRepo.Received(1).Update(Arg.Any<int>(), Arg.Any<ToDoItem>());
        }

        [Test]
        public async Task GivenThatUserIsAuthorized_whenPostToDoItemModelIsCalled_ItHitsAdd()
        {
            // Arrange
            var toDoItem = GetToDoItem();
            _context.Received(0).UpdateModel(toDoItem);
            await _toDoRepo.Received(0).Add(toDoItem);
            // Act
            await _sut.PostToDoItemModel(toDoItem);

            // Assert
            await _toDoRepo.Received(1).Add(Arg.Any<ToDoItem>());
        }

        [Test]
        public async Task GivenThatUserIsAuthorized_whenDeleteToDoItemModelIsCalled_ItHitsDelete()
        {
            // Arrange
            var toDoItem = GetToDoItem();
            _context.GetUserName().Returns("username");
            _toDoRepo.GetItemsById(Arg.Any<string>(), Arg.Any<int>()).Returns(toDoItem);

            await _toDoRepo.Received(0).Delete(toDoItem);
            // Act
            await _sut.DeleteToDoItemModel(toDoItem.ItemId);

            // Assert
            await _toDoRepo.Received(1).Delete(Arg.Any<ToDoItem>());
        }

        private static ToDoItem GetToDoItem()
        {
            return new ToDoItem
            {
                IsActive = true,
                ItemDescription = "Description",
                ItemId = 1,
                ItemName = "ItemName",
                LastUpdated = DateTime.UtcNow.ToString(),
                UserName = "UserName"
            };
        }
    }
}
