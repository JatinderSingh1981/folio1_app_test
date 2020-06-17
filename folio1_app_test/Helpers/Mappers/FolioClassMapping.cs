using folio1_app_test.Helpers.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace folio1_app_test.Helpers.Mappers
{
    public class FolioClassMapping : AutoMapper.Profile
    {
        public FolioClassMapping()
        {
            CreateMap<FolioClassDB, Models.FolioClass>();

            CreateMap<Models.FolioClass, FolioClassDB>();
        }
    }
}
