using PhoneBook.DAL.DB;
using PhoneBook.DAL.Repositories.Abstraction;
using PhoneBook.Models.Entities.Abstraction;

namespace PhoneBook.DAL.Repositories
{
  public class BaseRepository<T> : IRepository<T> where T : class
  {
    public readonly PhoneBookContext _context;
    public BaseRepository(PhoneBookContext context)
    {
      _context = context;
    }

    public virtual void Add(T item)
    {
      _context.Add(item);
    }

    public virtual async void Delete(T item)
    {
      _context.Remove(item);
    }

    public virtual IQueryable<T> Get(int id)
    {
      return this.Get().Where(x => ((IBaseEntity)x).Id == id);
    }

    public virtual IQueryable<T> Get()
    {
      var query = _context.Set<T>().AsQueryable();

      if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
      {
        query = query.Where(x => ((ISoftDelete)x).IsActive);
      }

      return query;
    }

    public async Task SaveChanges()
    {
      await _context.SaveChangesAsync();
    }

    public virtual void Update(T item)
    {
      _context.Update(item);
    }


  }
}
