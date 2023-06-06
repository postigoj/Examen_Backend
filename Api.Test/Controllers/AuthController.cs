using Api.Test.Models;
using Api.Test.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Api.Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
    
        private readonly AuthService _authService;


        public AuthController( AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UsuarioAcceso _userData)  {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


  
              var token = await _authService.AuthenticateUser(_userData.Usuario, _userData.Password);

                if (token != null)
                {
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Credenciales invalidas");
                }
            
            //else
            //{
            //    return BadRequest();
            //}
        }
    }
}
