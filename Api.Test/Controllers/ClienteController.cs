using Api.Test.Models;
using Api.Test.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using static Api.Test.Repository.ClienteRepository;

namespace Api.Test.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
       
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            return await _clienteService.GetClientesAsync();
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetById(int id)
        {

            {
                try
                {
                    var cliente = await _clienteService.getClienteDetailsAsync(id);

                    return cliente;
                }
                catch (NotFoundException)
                {
                    return NotFound(); 
                }
                catch (Exception ex)
                {
             Console.WriteLine(ex.Message);
            return StatusCode(500, "Ocurrió un error al buscar el cliente");

                }
            }
        }
        [HttpGet("dni/{dni}")]
        public async Task<ActionResult<Cliente>> GetByDni(int dni)
        {
            try
            {
                var cliente = await _clienteService.getDniAsync(dni);   

                return cliente;
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
             Console.WriteLine(ex.Message);
                return StatusCode(500, "Ocurrió un error al buscar el cliente");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> Post(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!cliente.IsValidCuit())
            {
                ModelState.AddModelError("Cuit", "Numero de cuit ivalido.");
                return BadRequest(ModelState);
            }

            bool isClienteValid = await _clienteService.IsClienteValidAsync(cliente);
            if (!isClienteValid)
            {
                return BadRequest("El DNI o Email ya existe.");
            }

            await _clienteService.CreateClienteAsync(cliente);

            return Ok("Cliente creado con éxito.");
        }
    }
}