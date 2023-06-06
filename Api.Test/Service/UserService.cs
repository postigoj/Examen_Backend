using Api.Test.Models;
using Api.Test.Repository;

namespace Api.Test.Service
{
    public class UserService
    {
        private readonly UserRepository _UserRepository;

        public UserService(UserRepository userRepository)
        {
            _UserRepository = userRepository;
        }

        public async Task<bool> CreateUserAsync(UsuarioAcceso user)
        {
            var checkUser = await _UserRepository.CheckUser(user.Usuario);

            if (checkUser)
            {
                return false;
            }

            await _UserRepository.AddUserAsync(user);
                return true;
            
        }

    }
}
