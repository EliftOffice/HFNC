using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using HFNC_Coaches.Business.BLL;
using HFNC_Coaches.Data.DTO;

namespace HFNC_Coaches.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsBLL _bll;

        public ProductsController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
            _bll = new ProductsBLL(connectionString);
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            List<ProductsDTO> data = _bll.GetAllProducts();
            return Ok(data);
        }
    }
}