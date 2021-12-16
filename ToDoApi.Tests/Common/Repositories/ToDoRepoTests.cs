using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using ToDoApi.Domain.Entities;
using ToDoApi.Infrastructure.Repositories;
using ToDoApi.Persistence.Context;

namespace ToDoApi.Tests.Common.Repositories
{
    [TestFixture]
    public class ToDoRepoTests
    {
        private ApplicationDbContext _inMemoryDbContext;
        private ToDoRepo _sut;
        [SetUp]
        public void Setup()
        {
            _inMemoryDbContext = InMemoryDbContextFactory.CreateInMemoryDbContext();
            _sut = new ToDoRepo(_inMemoryDbContext);
        }

        [Test]
        public async Task GivenATodoItemIsSavedInDb_WhenGetAllItems_ThenShouldReturnNewRecords()
        {
            // Arrange
            var todoItem = GenerateToDoItem();
            await _sut.Add(todoItem);

            // Act
            var actualItems = await _sut.GetAllItems(todoItem.UserName);

            // Assert
            actualItems.First().Should().BeEquivalentTo(todoItem);
        }

        [Test]
        public async Task GivenTodoItemIsSavedInDb_WhenGetItemsById_ThenShouldReturnOnePerticularRecord()
        {
            // Arrange
            var todoItem = GenerateToDoItem();
            await _sut.Add(todoItem);

            // Act
            var actualItem = await _sut.GetItemsById(todoItem.UserName, todoItem.ItemId);

            // Assert
            actualItem.Should().BeEquivalentTo(todoItem);
        }

        [Test]
        public async Task GivenTodoItemIsSavedInDb_WhenUpdate_ThenTheItemShouldBeUpdatedInDb()
        {
            // Arrange
            var todoItem = GenerateToDoItem();
            await _sut.Add(todoItem);
            var item = await _sut.GetItemsById(todoItem.UserName, todoItem.ItemId);
            item.IsActive = false;

            // Act
            await _sut.Update(item.ItemId, item);
            var actualItem = await _sut.GetItemsById(todoItem.UserName, todoItem.ItemId);

            // Assert
            actualItem.Should().BeEquivalentTo(todoItem);
            actualItem.IsActive.Should().Be(false);
        }

        [Test]
        public async Task GivenTodoItem_WhenSavedInDb_ThenGetItemsByIdShouldBeableToGetThatRecord()
        {
            // Arrange
            var todoItem = GenerateToDoItem();

            // Act
            await _sut.Add(todoItem);
            var actualItem = await _sut.GetItemsById(todoItem.UserName, todoItem.ItemId);

            // Assert
            actualItem.Should().BeEquivalentTo(todoItem);
        }

        [Test]
        public async Task GivenTodoItemIsSavedInDb_WhenDeleteIsCalled_ThenGetItemsByIdForThatObjectShouldReturnNull()
        {
            // Arrange
            var todoItem = GenerateToDoItem();
            await _sut.Add(todoItem);

            // Act
            await _sut.Delete(todoItem);
            var actualItem = await _sut.GetItemsById(todoItem.UserName, todoItem.ItemId);

            // Assert
            actualItem.Should().Be(null);
        }

        [TearDown]
        public void TearDown()
        {
            _inMemoryDbContext.Database.EnsureDeleted();
            _inMemoryDbContext.Dispose();
        }

        private static ToDoItem GenerateToDoItem()
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

        private static List<ToDoItem> GenerateToDoItemList()
        {
            var newItemList = new List<ToDoItem>();
            newItemList.Add(new ToDoItem
            {
                IsActive = true,
                ItemDescription = "Description",
                ItemId = 1,
                ItemName = "ItemName",
                LastUpdated = DateTime.UtcNow.ToString(),
                UserName = "UserName"
            });
            newItemList.Add(new ToDoItem
            {
                IsActive = true,
                ItemDescription = "Description1",
                ItemId = 2,
                ItemName = "ItemName1",
                LastUpdated = DateTime.UtcNow.ToString(),
                UserName = "UserName1"
            });
            return newItemList;
        }

    }
}
