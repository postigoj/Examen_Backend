using Api.Test.Models;

namespace Api.Test.Interface
{
    public interface IUserRepository
    {
        Task<UsuarioAcceso> GetUser(string username, string password);
        Task AddUserAsync(UsuarioAcceso user);

    }

}
