using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using folio1_app_test.BL;
using folio1_app_test.Models;
using folio1_app_test.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace folio1_app_test.Controllers
{
    //Testing build and release pipeline
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentBL studentBL;

        public StudentController(IStudentBL studentBL)
        {
            this.studentBL = studentBL;
        }

        // GET: api/<StudentController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await studentBL.GetAllStudentsAsync();
            return Ok(JsonConvert.SerializeObject(result,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await studentBL.GetStudentsAsync(id);
            return Ok(JsonConvert.SerializeObject(result,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
        }

        // POST api/<StudentController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest("Check the values being passed!!!");
            }

            var result = await studentBL.AddStudentAsync(student);
            return Ok(JsonConvert.SerializeObject(result,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Student student)
        {
            if (student == null || id == 0 || student.Id != id)
            {
                return BadRequest("Check the values being passed!!!");
            }
            var result = await studentBL.EditStudentAsync(id, student);
            return Ok(JsonConvert.SerializeObject(result,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Check the values being passed!!!");
            }
            var result = await studentBL.DeleteStudentAsync(id);
            return Ok(JsonConvert.SerializeObject(result,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
        }
    }
}
