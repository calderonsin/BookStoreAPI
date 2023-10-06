using AutoMapper;
using BookStoreAPI.Controllers;
using BookStoreAPI.DbContext;
using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreTest
{
    using System;
    using System.Runtime.InteropServices;
    using BookStoreAPI;
    using BookStoreAPI.DTOS;
    using Microsoft.AspNetCore.Mvc;
    using NUnit.Framework;



    public class GenreControllerTest : IDisposable
    {
        
        private DbContextOptions<BookStoreDbContext> options;
        private BookStoreDbContext BookStoreContext;
        private GenreController _GenreController;
        private Profile profile;
        private MapperConfiguration configuration;
        private  IMapper _mapper;     
        

        [OneTimeSetUp]
        public void OneTimeSetup() {
            profile = new MappingProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(configuration);

        }

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString(), new InMemoryDatabaseRoot())
                .Options;
            BookStoreContext = new BookStoreDbContext(options);            
            _GenreController = new GenreController(BookStoreContext,_mapper);
        }

        [TearDown]
        public void Cleanup()
        {
            Dispose();

        }

        [Test]
        public async Task ToDoController_GetToDoItems_ReturnList()
        {
            //Arrange
            var items = new List<Genre>()
            {
                new Genre{ Name = "Task 1", Description = "Description 1" },
                new Genre { Name = "Task 2", Description = "Description 2" },
                new Genre { Name = "Task 3", Description = "Description 3" },
            };
            using (var BookStoreDbContext = new BookStoreDbContext(options))
            {

                await BookStoreDbContext.AddRangeAsync(items);
                await BookStoreDbContext.SaveChangesAsync();

            };

            //Act
            var result = await _GenreController.GetGenres();
            var okresult = result as OkObjectResult;
            var genresDto = okresult.Value as List<GenreDTO>;


            //Assert
            Assert.That(genresDto,Is.Not.Null);
            Assert.That(genresDto, Has.Count.EqualTo(3));
            //Assert.That(genresDto.Count, Is.EqualTo(3));
            //Assert.That(result., Is.EqualTo(3));


        }

        public void Dispose()
        {
            {
                using (var context = new BookStoreDbContext(options))
                {
                    if (context.Database.IsInMemory())
                    {
                        context.Database.EnsureDeleted();
                    }
                    
                }
            }
        }
    }
}
