using PhoneBook.DAL.DB;
using PhoneBook.Models.Entities;
using PhoneBook.DAL.Repositories.Abstraction;

namespace PhoneBook.DAL.Repositories
{
    public class UserRepository : BaseRepository<SystemUser>, IUserRepository
    {
        private readonly PhoneBookContext _context;
        public UserRepository(PhoneBookContext context) : base(context)
        {
            _context = context;
        }
    }
}
