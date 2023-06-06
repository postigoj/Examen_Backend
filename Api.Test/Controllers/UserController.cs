using Api.Test.Models;
using Api.Test.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioAcceso>> Post(UsuarioAcceso user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

         var result =await _userService.CreateUserAsync(user);
            if (result)
            {
            return Ok("Usuario creado con exito");
            }
            return BadRequest("Usuario ya existente");
        }
    }
}
