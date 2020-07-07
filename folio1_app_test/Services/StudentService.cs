using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using folio1_app_test.Models;
using AutoMapper;
using folio1_app_test.Helpers.DB;
using Microsoft.EntityFrameworkCore;

namespace folio1_app_test.Services
{
    public interface IStudentService
    {
        Task<(IEnumerable<Student> Students, bool IsSuccess, string Message)> GetAllStudentsAsync();
        Task<(IEnumerable<Student> Students, bool IsSuccess, string Message)> GetStudentsAsync(int folioClassId);
        Task<(Student Student, bool IsSuccess, string Message)> GetStudentAsync(int studentId);
        Task<(bool IsSuccess, string Message)> CheckDuplicateAsync(int studentId, string studentLastName);
        Task<(Student student, bool IsSuccess, string Message)> AddStudentAsync(Student student);
        Task<(Student student, bool IsSuccess, string Message)> EditStudentAsync(int id, Student student);
        Task<(Student student, bool IsSuccess, string Message)> DeleteClassStudentsAsync(int id);
        Task<(Student student, bool IsSuccess, string Message)> DeleteStudentAsync(int id);
    }
    public class StudentService : IStudentService
    {
        private readonly DataContext dbContext;
        private readonly IMapper mapper;

        public StudentService(DataContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
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

        public async Task<(IEnumerable<Student> Students, bool IsSuccess, string Message)> GetAllStudentsAsync()
        {
            try
            {
                var students = await dbContext.Students.ToListAsync();
                if (students != null && students.Any())
                {
                    var result = mapper.Map<IEnumerable<Student>>(students);
                    return (result, true, null);
                }
                return (null, false, "Not found");
            }
            catch (Exception ex)
            {

                return (null, false, ex.Message);
            }
        }

        public async Task<(IEnumerable<Student> Students, bool IsSuccess, string Message)> GetStudentsAsync(int folioClassId)
        {
            try
            {
                var students = await dbContext.Students.Where(x => x.FolioClassId == folioClassId).ToListAsync();
                if (students != null && students.Any())
                {
                    var result = mapper.Map<IEnumerable<Student>>(students);
                    return (result, true, null);
                }
                return (null, false, "Not found");
            }
            catch (Exception ex)
            {

                return (null, false, ex.Message);
            }
        }

        public async Task<(Student Student, bool IsSuccess, string Message)> GetStudentAsync(int studentId)
        {
            try
            {
                var student = await dbContext.Students.Where(x => x.Id == studentId).FirstOrDefaultAsync();
                if (student != null)
                {
                    var result = mapper.Map<Student>(student);
                    return (result, true, null);
                }
                return (null, false, "Not found");
            }
            catch (Exception ex)
            {

                return (null, false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string Message)> CheckDuplicateAsync(int studentId, string studentLastName)
        {
            try
            {
                string lastNameErrorMessage = "The surname needs to be unique. Please enter another surname";
                StudentDB student = null;
                if (studentId > 0)
                {
                    student = await dbContext.Students.Where(x => x.LastName == studentLastName && x.Id != studentId).FirstOrDefaultAsync();
                }
                else
                {
                    student = await dbContext.Students.Where(x => x.LastName == studentLastName).FirstOrDefaultAsync();
                }

                //If not null, that means another student with the same lastname is found
                if (student != null)
                {
                    var result = mapper.Map<Student>(student);
                    return (false, lastNameErrorMessage);
                }

                return (true, "Success!!!");
            }
            catch (Exception ex)
            {

                return (false, ex.Message);
            }
        }

        public async Task<(Student student, bool IsSuccess, string Message)> AddStudentAsync(Student student)
        {
            try
            {
                if (student != null)
                {
                    student.Id = dbContext.Students.MaxAsync(x => x.Id).Result + 1;
                    var result = mapper.Map<StudentDB>(student);
                    dbContext.Students.Add(result);
                    await dbContext.SaveChangesAsync();

                    student = mapper.Map<Student>(result);
                    return (student, true, "Success!!!");
                }
                return (null, false, "Not found");
            }
            catch (Exception ex)
            {
                return (null, false, ex.Message);
            }
        }

        public async Task<(Student student, bool IsSuccess, string Message)> EditStudentAsync(int id, Student student)
        {
            try
            {
                if (student != null && student.Id > 0 && id > 0)
                {
                    var result = await dbContext.Students.Where(x => x.Id == id).FirstOrDefaultAsync();
                    if (result != null)
                    {
                        dbContext.Entry(result).CurrentValues.SetValues(student);
                        await dbContext.SaveChangesAsync();
                        return (null, true, "Success!!!");
                    }
                }
                return (null, false, "Not found");
            }
            catch (Exception ex)
            {
                return (null, false, ex.Message);
            }
        }

        public async Task<(Student student, bool IsSuccess, string Message)> DeleteClassStudentsAsync(int id)
        {
            try
            {
                if (id > 0)
                {
                    //var mappedClass = mapper.Map<FolioClassDB>(folioClass);
                    var result = await dbContext.Students.Where(x => x.FolioClassId == id).ToListAsync();
                    if (result != null && result.Any())
                    {
                        dbContext.Students.RemoveRange(result);
                        await dbContext.SaveChangesAsync();
                        return (null, true, "Success!!!");
                    }
                }
                return ( null, true, "No record found");
            }
            catch (Exception ex)
            {
                return (null, false, ex.Message);
            }
        }

        public async Task<(Student student, bool IsSuccess, string Message)> DeleteStudentAsync(int id)
        {
            try
            {
                if (id > 0)
                {
                    //var mappedClass = mapper.Map<FolioClassDB>(folioClass);
                    var result = await dbContext.Students.Where(x => x.Id == id).FirstOrDefaultAsync();
                    if (result != null)
                    {
                        dbContext.Students.Remove(result);
                        await dbContext.SaveChangesAsync();
                        return (null, true, "Success!!!");
                    }
                }
                return (null, false, "No record found");
            }
            catch (Exception ex)
            {
                return (null, false, ex.Message);
            }
        }
    }
}
