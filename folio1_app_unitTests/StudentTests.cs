using System;
using Xunit;
using AutoMapper;
using folio1_app_test.Helpers.Mappers;
using folio1_app_test.Helpers.DB;
using folio1_app_test.Services;
using folio1_app_test.Controllers;
using folio1_app_test.BL;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace folio1_app_unitTests
{
    public class StudentTests
    {
        DbContextOptions<DataContext> options;
        DataContext dbContext;
        StudentMapping studentMapping;
        MapperConfiguration configuration;
        Mapper mapper;
        StudentService sService;

        /// <summary>
        /// Initialize all the resources required here
        /// </summary>
        public StudentTests()
        {
            options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(nameof(StudentTests))
                .Options;
            dbContext = new DataContext(options);
            CreateStudents(dbContext);

            studentMapping = new StudentMapping();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(studentMapping));
            mapper = new Mapper(configuration);

            sService = new StudentService(dbContext, mapper);
        }

        //public void Dispose()
        //{
        //    dbContext.DisposeAsync();
        //}

        private void CreateStudents(DataContext dbContext)
        {
            if (!dbContext.Students.Any())
            {
                dbContext.Students.Add(new StudentDB() { Id = 1, FolioClassId = 1, FirstName = "John", LastName = "Packer", Age = 18, GPA = 3.2 });
                dbContext.Students.Add(new StudentDB() { Id = 2, FolioClassId = 1, FirstName = "Peter", LastName = "Johnston", Age = 19, GPA = 2.5 });
                dbContext.Students.Add(new StudentDB() { Id = 3, FolioClassId = 1, FirstName = "Robert", LastName = "Smith", Age = 20, GPA = 3.1 });
                dbContext.Students.Add(new StudentDB() { Id = 4, FolioClassId = 1, FirstName = "Louise", LastName = "Thomstone", Age = 21, GPA = 2.1 });

                dbContext.Students.Add(new StudentDB() { Id = 5, FolioClassId = 2, FirstName = "JohnP", LastName = "JPacker", Age = 18, GPA = 3.3 });
                dbContext.Students.Add(new StudentDB() { Id = 6, FolioClassId = 2, FirstName = "PeterJ", LastName = "PJohnston", Age = 19, GPA = 2.5 });
                dbContext.Students.Add(new StudentDB() { Id = 7, FolioClassId = 2, FirstName = "RobertS", LastName = "RSmith", Age = 20, GPA = 3.2 });
                dbContext.Students.Add(new StudentDB() { Id = 8, FolioClassId = 2, FirstName = "LouiseT", LastName = "LThomstone", Age = 21, GPA = 3.9 });
                dbContext.SaveChanges();
            }
        }

        [Fact]
        public async Task CheckDuplicateStudentSurname_Add()
        {
            StudentBL sAPI = new StudentBL(sService);
            var result = await sAPI.AddStudentAsync(new folio1_app_test.Models.Student { Id = 0, FolioClassId = 4, FirstName = "Johnathon", LastName = "Packer", Age = 18, GPA = 3.2 });
            //Packer surname is already added so it wont allow another Packer to be added
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task CheckDuplicateStudentSurname_Add2()
        {
            StudentBL sAPI = new StudentBL(sService);
            var result = await sAPI.AddStudentAsync(new folio1_app_test.Models.Student { Id = 0, FolioClassId = 4, FirstName = "Johnathon", LastName = "PackerNew", Age = 18, GPA = 3.2 });
            //PackerNew surname is not there in the collection so it will allow surname to be added
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task CheckDuplicateStudentSurname_Edit()
        {
            StudentBL sAPI = new StudentBL(sService);
            var result = await sAPI.AddStudentAsync(new folio1_app_test.Models.Student { Id = 1, FolioClassId = 4, FirstName = "Johnathon", LastName = "Packer", Age = 18, GPA = 3.2 });
            //Updating the student with its own lastname, so it should allow
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task CheckDuplicateStudentSurname_Edit2()
        {
            StudentBL sAPI = new StudentBL(sService);
            var result = await sAPI.AddStudentAsync(new folio1_app_test.Models.Student { Id = 1, FolioClassId = 4, FirstName = "Johnathon", LastName = "Johnston", Age = 18, GPA = 3.2 });
            //Updating the student with someone else's lastname, so it should not allow
            Assert.False(result.IsSuccess);
        }

    }
}
