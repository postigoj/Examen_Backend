using Api.Test.Interface;
using Api.Test.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Test.Repository
{
    public class UserRepository : IUserRepository
    {
        readonly DataBaseContext _dbContext = new();

        public UserRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserAsync(UsuarioAcceso user)
        {
            {
                try
                {
                   var userExist = await _dbContext.UsuarioAccesos.AnyAsync(u => u.Usuario == user.Usuario);

              
                    _dbContext.UsuarioAccesos.Add(user);
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Fallo al regristar usuario", ex);
                }
            }
        }

        public async Task<UsuarioAcceso> GetUser(string usuario, string password)
        {
            return await _dbContext.UsuarioAccesos.FirstOrDefaultAsync(u => u.Usuario == usuario && u.Password == password );
        }

        public async Task<bool> CheckUser(string usuario)
        {
            var userExist = await _dbContext.UsuarioAccesos.AnyAsync(u => usuario == u.Usuario);

            return userExist;

        }

    }
}
