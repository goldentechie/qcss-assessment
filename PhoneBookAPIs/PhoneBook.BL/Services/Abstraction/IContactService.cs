using PhoneBook.Models.DTOs.Contacts;

namespace PhoneBook.BL.Services.Abstraction
{
  public interface IContactService
  {
    Task<IEnumerable<ContactGetDto>> GetAsync();
    Task<ContactGetDto?> GetAsync(int id);
    Task<ContactGetDto> PostAsync(ContactPostDto data);
    Task<ServiceResponse> PutAsync(int id, ContactPostDto data);
    Task<ServiceResponse> DeleteAsync(int id);
    Task<bool> ExistsAsync(string phoneNumber);

  }
}
