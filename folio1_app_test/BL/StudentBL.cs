using folio1_app_test.Models;
using folio1_app_test.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace folio1_app_test.BL
{
    public interface IStudentBL
    {
        Task<(IEnumerable<Student> Students, bool IsSuccess, string Message)> GetStudentsAsync(int FolioClassId);
        Task<(Student Student, bool IsSuccess, string Message)> GetStudentAsync(int StudentId);
        Task<(bool IsSuccess, string Message)> CheckDuplicateAsync(int StudentId, string studentLastName);
        Task<(Student student, bool IsSuccess, string Message)> AddStudentAsync(Student student);
        Task<(bool IsSuccess, string Message)> EditStudentAsync(int id, Student student);
        Task<(bool IsSuccess, string Message)> DeleteStudentAsync(int id);
    }
    public class StudentBL : IStudentBL
    {
        private readonly IStudentService studentService;

        public StudentBL(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public async Task<(IEnumerable<Student> Students, bool IsSuccess, string Message)> GetStudentsAsync(int FolioClassId)
        {
            return await this.studentService.GetStudentsAsync(FolioClassId);
        }
        public async Task<(Student Student, bool IsSuccess, string Message)> GetStudentAsync(int StudentId)
        {
            return await this.studentService.GetStudentAsync(StudentId);
        }
        public async Task<(bool IsSuccess, string Message)> CheckDuplicateAsync(int StudentId, string studentLastName)
        {
            return await this.studentService.CheckDuplicateAsync(StudentId, studentLastName);
        }
        public async Task<(Student student, bool IsSuccess, string Message)> AddStudentAsync(Student student)
        {
            var checkDuplicate = await CheckDuplicateAsync(student.Id, student.LastName);
            if (!checkDuplicate.IsSuccess)
            {
                return (null, false, checkDuplicate.Message);
            }
            return await this.studentService.AddStudentAsync(student);
        }
        public async Task<(bool IsSuccess, string Message)> EditStudentAsync(int id, Student student)
        {
            var checkDuplicate = await CheckDuplicateAsync(student.Id, student.LastName);
            if (!checkDuplicate.IsSuccess)
            {
                return (false, checkDuplicate.Message);
            }
            return await this.studentService.EditStudentAsync(id, student);
        }
        public async Task<(bool IsSuccess, string Message)> DeleteStudentAsync(int id)
        {
            return await this.studentService.DeleteStudentAsync(id);
        }
    }
}
