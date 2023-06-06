using Api.Test.Interface;
using Api.Test.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Test.Repository
{
    public class ClienteRepository : IClientes
    {
         readonly DataBaseContext _dbContext = new();

        public ClienteRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message)
            {
            }
        }

        public async Task<List<Cliente>> GetClientesDetailsAsync()
        {
            try
            {
                return await _dbContext.Clientes.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Cliente> GetClienteDetailsAsync(int id)
        {

            try
            {
                Cliente cliente = await _dbContext.Clientes.FindAsync(id);

                if (cliente != null)
                {
                    return cliente;
                }
                else
                {
                    throw new NotFoundException("Cliente no encontrado");
                }
            }
            catch (NotFoundException)
            {
                throw; 
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al buscar el cliente.", ex);
            }

        }

        public async Task<Cliente> getDniAsync(int dni)
        {
            try
            {
                Cliente cliente = await _dbContext.Clientes.FirstOrDefaultAsync(c => c.Dni == dni);
                if (cliente != null)
                {
                    return cliente;
                }
                else
                {
                    throw new NotFoundException("Cliente no encontrado");
                }
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al buscar el cliente", ex);
            }
        }

        public async Task AddClienteAsync(Cliente cliente)
        {
            try
            {
                _dbContext.Clientes.Add(cliente);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Fallo en agregar cliente", ex);
            }
        }

        public async Task<bool> IsDniRepeatedAsync(long dni)
        {
            return await _dbContext.Clientes.AnyAsync(c => c.Dni == dni);
        }

        public async Task<bool> IsEmailRepeatedAsync(string email)
        {
            return await _dbContext.Clientes.AnyAsync(c => c.Email == email);
        }

    }
}