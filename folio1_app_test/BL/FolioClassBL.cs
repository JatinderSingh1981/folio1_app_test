using folio1_app_test.Models;
using folio1_app_test.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace folio1_app_test.BL
{
    public interface IFolioClassBL
    {
        Task<(IEnumerable<FolioClass> FolioClasses, bool IsSuccess, string Message)> GetFolioClassesAsync();
        Task<(FolioClass folioClass, bool IsSuccess, string Message)> AddFolioClassAsync(FolioClass folioClass);

        Task<(FolioClass folioClass, bool IsSuccess, string Message)> EditFolioClassAsync(int id, FolioClass folioClass);

        Task<(FolioClass folioClass, bool IsSuccess, string Message)> DeleteFolioClassAsync(int id);
    }
    public class FolioClassBL : IFolioClassBL
    {
        private readonly IFolioClassService folioClassService;
        private readonly IStudentService studentService;

        public FolioClassBL(IFolioClassService folioClassService, IStudentService studentService)
        {
            this.folioClassService = folioClassService;
            this.studentService = studentService;
        }

        public async Task<(IEnumerable<FolioClass> FolioClasses, bool IsSuccess, string Message)> GetFolioClassesAsync()
        {
            return await folioClassService.GetFolioClassesAsync();
        }
        public async Task<(FolioClass folioClass, bool IsSuccess, string Message)> AddFolioClassAsync(FolioClass folioClass)
        {
            return await folioClassService.AddFolioClassAsync(folioClass);
        }

        public async Task<(FolioClass folioClass, bool IsSuccess, string Message)> EditFolioClassAsync(int id, FolioClass folioClass)
        {
            return await folioClassService.EditFolioClassAsync(id, folioClass);
        }

        public async Task<(FolioClass folioClass, bool IsSuccess, string Message)> DeleteFolioClassAsync(int id)
        {
            var result = await studentService.DeleteClassStudentsAsync(id);
            if (result.IsSuccess)
                return await folioClassService.DeleteFolioClassAsync(id);
            else
                return (null, result.IsSuccess, result.Message);
        }
    }
}
