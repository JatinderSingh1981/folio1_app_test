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
    [Route("api/folioclasses")]
    [ApiController]
    public class FolioClassController : ControllerBase
    {
        private readonly IFolioClassBL folioClassBL;

        public FolioClassController(IFolioClassBL folioClassBL)
        {
            this.folioClassBL = folioClassBL;
        }
        // GET: api/<FolioClassController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await folioClassBL.GetFolioClassesAsync();
            return Ok(JsonConvert.SerializeObject(result,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
        }

        // GET api/<FolioClassController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FolioClassController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FolioClass folioClass)
        {
            if (folioClass == null)
            {
                return BadRequest("Check the values being passed!!!");
            }

            var result = await folioClassBL.AddFolioClassAsync(folioClass);
            return Ok(JsonConvert.SerializeObject(result,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
        }

        // PUT api/<FolioClassController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FolioClass folioClass)
        {
            if (folioClass == null || id == 0 || folioClass.Id != id)
            {
                return BadRequest("Check the values being passed!!!");
            }
            var result = await folioClassBL.EditFolioClassAsync(id, folioClass);
            return Ok(JsonConvert.SerializeObject(result,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
        }

        // DELETE api/<FolioClassController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Check the values being passed!!!");
            }
            var result = await folioClassBL.DeleteFolioClassAsync(id);
            return Ok(JsonConvert.SerializeObject(result,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
        }
    }
}
