using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using HFNC_Coaches.Business.BLL;
using HFNC_Coaches.Data.DTO;

namespace HFNC_Coaches.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleRegistryController : ControllerBase
    {
        private readonly PeopleRegistryBLL _bll;

        public PeopleRegistryController(IConfiguration configuration)
        {
            // Make sure your appsettings.json has "DefaultConnection" for MySQL
            string connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
            _bll = new PeopleRegistryBLL(connectionString);
        }

        [HttpPost]
        [Route("Save")]
        public IActionResult Save([FromBody] PeopleRegistryDTO dto)
        {
            if (dto == null) return BadRequest("Invalid data");
            bool isSaved = _bll.AddPerson(dto);
            if (isSaved) return Ok(new { message = "Saved successfully!" });
            return StatusCode(500, "Error saving data");
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            List<PeopleRegistryDTO> data = _bll.GetAllPeople();
            return Ok(data);
        }
    }
}