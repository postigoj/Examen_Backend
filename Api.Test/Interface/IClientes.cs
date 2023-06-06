using Api.Test.Models;

namespace Api.Test.Interface
{
    public interface IClientes
    {
        Task<List<Cliente>> GetClientesDetailsAsync();
        Task<Cliente> GetClienteDetailsAsync(int id);
        Task<Cliente> getDniAsync(int dni);
        Task AddClienteAsync(Cliente cliente);
        Task<bool> IsDniRepeatedAsync(long dni);
        Task<bool> IsEmailRepeatedAsync(string email);
    }
}