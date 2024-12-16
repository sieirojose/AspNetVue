using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AspNetVueApp.Application.Interfaces;
using AspNetVueApp.Application.Services;
using AspNetVueApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AspNetVueApp.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] User user)
        {
            await _userService.AddUserAsync(user);
            return Ok("User added successfully");
        }
    }
}
