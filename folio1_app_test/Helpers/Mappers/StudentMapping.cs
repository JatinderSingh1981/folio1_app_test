using folio1_app_test.Helpers.DB;
using folio1_app_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace folio1_app_test.Helpers.Mappers
{
    public class StudentMapping : AutoMapper.Profile
    {
        public StudentMapping()
        {
            CreateMap<StudentDB, Models.Student>();

            CreateMap<Models.Student, StudentDB>();
        }
    }

}
