using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetVueApp.Application.Services;
using System.Threading.Tasks;

namespace AspNetVueApp.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatFactsController : ControllerBase
    {
        private readonly CatFactService _catFactService;

        public CatFactsController(CatFactService catFactService)
        {
            _catFactService = catFactService;
        }

        [HttpGet("random-fact")]
        public async Task<IActionResult> GetRandomCatFact()
        {
            var fact = await _catFactService.GetRandomCatFactAsync();
            return Ok(new { Fact = fact });
        }
    }
}
