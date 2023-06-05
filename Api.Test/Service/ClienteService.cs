using Api.Test.Models;
using Api.Test.Repository;

namespace Api.Test.Service
{

    public class ClienteService
    {
        private readonly ClienteRepository _clienteRepository;

        public ClienteService(ClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<List<Cliente>> GetClientesAsync()
        {
            return await _clienteRepository.GetClientesDetailsAsync();
        }


        public async Task<Cliente> getClienteDetailsAsync(int id)
        {
            return await _clienteRepository.GetClienteDetailsAsync(id);
        }


        public async Task<Cliente> getDniAsync(int dni)
        {
            return await _clienteRepository.getDniAsync(dni);
        }

        public async Task CreateClienteAsync(Cliente cliente)
        {
            await _clienteRepository.AddClienteAsync(cliente);
        }

        public async Task<bool> IsClienteValidAsync(Cliente cliente)
        {
            bool isDniRepeated = await _clienteRepository.IsDniRepeatedAsync(cliente.Dni);
            bool isEmailRepeated = await _clienteRepository.IsEmailRepeatedAsync(cliente.Email);

            return !isDniRepeated && !isEmailRepeated;
        }
    }
}
