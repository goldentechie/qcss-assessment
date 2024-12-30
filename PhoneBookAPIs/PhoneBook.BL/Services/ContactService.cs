using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhoneBook.BL.Services.Abstraction;
using PhoneBook.DAL.Repositories.Abstraction;
using PhoneBook.Models.DTOs.Contacts;
using PhoneBook.Models.Entities;

namespace PhoneBook.BL.Services
{
  public enum ServiceResponse
  {
    NotFound,
    Success
  }

  public class ContactService : IContactService
  {
    private readonly IRepository<Contact> _repository;
    private readonly IMapper _mapper;

    public ContactService(IRepository<Contact> repository, IMapper mapper)
    {
      this._repository = repository;
      this._mapper = mapper;
    }

    public async Task<ServiceResponse> DeleteAsync(int id)
    {
      var contact = await this._repository.Get(id).FirstOrDefaultAsync();
      if (contact == null)
      {
        return ServiceResponse.NotFound;
      }
      else
      {
        try
        {
          _repository.Delete(contact);
          await _repository.SaveChanges();
          return ServiceResponse.Success;
        }
        catch
        {

          throw;
        }
      }
    }

    public async Task<bool> ExistsAsync(string phoneNumber)
    {
      return await this._repository.Get().AnyAsync(x => x.PhoneNumber == phoneNumber);
    }

    public async Task<IEnumerable<ContactGetDto>> GetAsync()
    {
      var contacts = await this._repository.Get().ToListAsync();
      return _mapper.Map<IEnumerable<ContactGetDto>>(contacts);
    }

    public async Task<ContactGetDto?> GetAsync(int id)
    {
      var contact = await this._repository.Get(id).FirstOrDefaultAsync();

      if (contact == null)
      {
        return null;
      }

      return _mapper.Map<ContactGetDto>(contact);
    }

    public async Task<ContactGetDto> PostAsync(ContactPostDto data)
    {
      var contact = _mapper.Map<Contact>(data);

      try
      {
        _repository.Add(contact);
        await _repository.SaveChanges();

        return _mapper.Map<ContactGetDto>(contact);
      }
      catch
      {
        throw;
      }
    }

    public async Task<ServiceResponse> PutAsync(int id, ContactPostDto data)
    {
      var contact = await this._repository.Get(id).FirstOrDefaultAsync();
      if (contact == null)
      {
        return ServiceResponse.NotFound;
      }
      else
      {
        contact.PhoneNumber = data.PhoneNumber;
        contact.FirstName = data.FirstName;
        contact.LastName = data.LastName;

        _repository.Update(contact);

        try
        {
          await _repository.SaveChanges();
          return ServiceResponse.Success;
        }
        catch
        {

          throw;
        }
      }
    }
  }
}
