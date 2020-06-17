using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace folio1_app_test.Models
{
    public class FolioClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string TeacherName { get; set; }
    }
}
