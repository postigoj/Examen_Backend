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
    }
}
