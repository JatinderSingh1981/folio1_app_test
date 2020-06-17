using AutoMapper;
using folio1_app_test.Helpers.DB;
using folio1_app_test.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace folio1_app_test.Services
{
    public interface IFolioClassService
    {
        Task<(IEnumerable<FolioClass> FolioClasses, bool IsSuccess, string Message)> GetFolioClassesAsync();
        Task<(FolioClass folioClass, bool IsSuccess, string Message)> AddFolioClassAsync(FolioClass folioClass);

        Task<(bool IsSuccess, string Message)> EditFolioClassAsync(int id, FolioClass folioClass);

        Task<(bool IsSuccess, string Message)> DeleteFolioClassAsync(int id);
    }
    public class FolioClassService : IFolioClassService
    {
        private readonly DataContext dbContext;
        private readonly IMapper mapper;

        public FolioClassService(DataContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.FolioClasses.Any())
            {
                dbContext.FolioClasses.Add(new FolioClassDB() { Id = 1, Name = "UI/UX", Location = "Building 5 Room 501", TeacherName = "Mr. Johnson" });
                dbContext.FolioClasses.Add(new FolioClassDB() { Id = 2, Name = "DevOps", Location = "Building 2 Room 201", TeacherName = "Miss Thomson" });
                dbContext.FolioClasses.Add(new FolioClassDB() { Id = 3, Name = "Azure", Location = "Building 3 Room 301", TeacherName = "Mr. ABX" });
                dbContext.FolioClasses.Add(new FolioClassDB() { Id = 4, Name = ".Net Core", Location = "Building 6 Room 602", TeacherName = "Miss xyz" });
                dbContext.SaveChanges();
            }
        }

        public async Task<(IEnumerable<FolioClass> FolioClasses, bool IsSuccess, string Message)> GetFolioClassesAsync()
        {
            try
            {
                var classes = await dbContext.FolioClasses.ToListAsync();
                if (classes != null && classes.Any())
                {
                    var result = mapper.Map<IEnumerable<FolioClass>>(classes);
                    return (result, true, "Success!!!");
                }
                return (null, true, "Not found");
            }
            catch (Exception ex)
            {

                return (null, false, ex.Message);
            }
        }

        public async Task<(FolioClass folioClass, bool IsSuccess, string Message)> AddFolioClassAsync(FolioClass folioClass)
        {
            try
            {
                if (folioClass != null)
                {
                    folioClass.Id = dbContext.FolioClasses.MaxAsync(x => x.Id).Result + 1;
                    var mappedClass = mapper.Map<FolioClassDB>(folioClass);
                    dbContext.FolioClasses.Add(mappedClass);
                    await dbContext.SaveChangesAsync();

                    folioClass = mapper.Map<FolioClass>(mappedClass);
                    return (folioClass, true, "Success!!!");
                }
                return (null, false, "Not found");
            }
            catch (Exception ex)
            {
                return (null, false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string Message)> EditFolioClassAsync(int id, FolioClass folioClass)
        {
            try
            {
                if (folioClass != null && folioClass.Id > 0 && id > 0)
                {
                    var result = await dbContext.FolioClasses.Where(x => x.Id == id).FirstOrDefaultAsync();
                    if (result != null)
                    {
                        dbContext.Entry(result).CurrentValues.SetValues(folioClass);
                        await dbContext.SaveChangesAsync();
                        return (true, "Success!!!");
                    }
                }
                return (false, "Not found");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string Message)> DeleteFolioClassAsync(int id)
        {
            try
            {
                if (id > 0)
                {
                    //var mappedClass = mapper.Map<FolioClassDB>(folioClass);
                    var result = await dbContext.FolioClasses.Where(x => x.Id == id).FirstOrDefaultAsync();
                    if (result != null)
                    {
                        dbContext.FolioClasses.Remove(result);
                        await dbContext.SaveChangesAsync();
                        return (true, "Success!!!");
                    }
                }
                return (true, "Not Record found");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
