using Microsoft.AspNetCore.Mvc;
using System;
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
          //  string connectionString = configuration.GetConnectionString("DefaultConnection") ?? "server=localhost;port=3306;database=hfnc;user=root;password=Hfnc@2026;";
            string connectionString = "server=mibu9ojpzcmahn1uxysoyoqf;port=3306;database=hfnc;user=root;password=Hfnc@2026;";
            string connectionStringV1 = configuration.GetConnectionString("DefaultConnection") ?? "";
            _bll = new PeopleRegistryBLL(connectionString);
        }

        [HttpPost]
        [Route("Save")]
        public IActionResult Save([FromBody] PeopleRegistryDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Invalid data");
                bool isSaved = _bll.AddPerson(dto);
                if (isSaved) return Ok(new { message = "Saved successfully!" });
                return StatusCode(500, "Error saving data");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Save Controller Error: {ex.Message}");
                return StatusCode(500, new { message = $"An internal error occurred: {ex.Message}" });
            }
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            List<PeopleRegistryDTO> data = _bll.GetAllPeople();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var person = _bll.GetPersonById(id);
                if (person == null)
                {
                    return NotFound(new { message = "Person not found." });
                }
                return Ok(person);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Get(id) Controller Error: {ex.Message}");
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] PeopleRegistryDTO dto)
        {
            try
            {
                if (dto == null || dto.Id == 0)
                {
                    return BadRequest("Invalid data for update.");
                }
                bool isUpdated = _bll.UpdatePerson(dto);
                if (isUpdated)
                {
                    return Ok(new { message = "Updated successfully!" });
                }
                return NotFound(new { message = "Person not found or could not be updated." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update Controller Error: {ex.Message}");
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid ID.");
                }
                bool isDeleted = _bll.DeletePerson(id);
                if (isDeleted)
                {
                    return Ok(new { message = "Deleted successfully!" });
                }
                return NotFound(new { message = "Person not found or could not be deleted." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Delete Controller Error: {ex.Message}");
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }
    }
}