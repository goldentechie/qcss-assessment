using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.DAL.Repositories.Abstraction
{
    public interface IRepository<T>
    {
        IQueryable<T> Get(int id);
        IQueryable<T> Get();
        void Add(T item);
        void Delete(T item);
        void Update(T item);
        Task SaveChanges();

    }
}
