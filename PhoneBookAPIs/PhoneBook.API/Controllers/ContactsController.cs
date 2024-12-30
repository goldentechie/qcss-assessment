using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.API.Models;
using PhoneBook.BL.Services.Abstraction;
using PhoneBook.DAL.DB;
using PhoneBook.Models.DTOs.Contacts;
using PhoneBook.Models.Entities;

namespace PhoneBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactsController(IContactService contactService)
        {
            this._contactService = contactService;
        }

        // GET: api/<ContactsController>
        [HttpGet]
        public async Task<APIResponse<IEnumerable<ContactGetDto>>> Get()
        {

      var contacts = await this._contactService.GetAsync();
            
            return new APIResponse<IEnumerable<ContactGetDto>>()
            {
                IsError = false,
                Message = "",
                data = contacts
            };
        }

        // GET api/<ContactsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
      var contact = await this._contactService.GetAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(new APIResponse<ContactGetDto>
            {
                IsError = false,
                Message = "",
                data = contact
            });
        }

        // POST api/<ContactsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactPostDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var contact = await this._contactService.PostAsync(model);
                    return Created("", contact);
                }
                catch
                {
                    return BadRequest();
                }
            }

            return BadRequest(ModelState);
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ContactPostDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await this._contactService.PutAsync(id, model);
            if (result == BL.Services.ServiceResponse.NotFound)
                return NotFound();

            return Ok();
        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await this._contactService.DeleteAsync(id);
            if (result == BL.Services.ServiceResponse.NotFound)
                return NotFound();

            return Ok(new APIResponse<object>());
        }

        [Route("entityexists/{data}")]
        [HttpGet]
        public async Task<bool> EntityExists(string data)
        {
            return await this._contactService.ExistsAsync(data);
        }
    }
}
