using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rubicon.Demo.Api.Controllers.V1.Dto.Mappers;
using Rubicon.Demo.Api.Controllers.V1.Mappers;
using Rubicon.Demo.Api.Domain;
using Rubicon.Demo.Api.Services.Interfaces;

namespace Rubicon.Demo.Api.Controllers.V1
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Dto.Contact>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {

            Result<IEnumerable<Contact>> result = await _contactService.GetAsync();

            if (result.IsSuccess)
            {
                return Ok(Dto.Mappers.ContactMapper.Map(result.Value));
            }

            return StatusCodeMapper.Map(result.Status, result.Message);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Dto.Contact))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(Guid id)
        {
            Result<Contact> result = await _contactService.GetAsync(id);

            if (result.IsSuccess)
            {
                return Ok(ContactMapper.Map(result.Value));
            }

            return StatusCodeMapper.Map(result.Status, result.Message);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Dto.Contact))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] Dto.ContactForUpsert model)
        {
            Contact contact = ContactMapper.Map(model);

            Result<Contact> result = await _contactService.CreateAsync(contact);

            if (result.IsSuccess)
            {
                return Ok(ContactMapper.Map(result.Value));
            }

            return StatusCodeMapper.Map(result.Status, result.Message);
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Dto.Contact))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, [FromBody] Dto.ContactForUpsert model)
        {
            Contact contact = ContactMapper.Map(model);

            Result<Contact> result = await _contactService.UpdateAsync(id, contact);

            if (result.IsSuccess)
            {
                return Ok(ContactMapper.Map(result.Value));
            }

            return StatusCodeMapper.Map(result.Status, result.Message);
        }


        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Dto.Contact))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            Result<Contact> result = await _contactService.DeleteAsync(id);

            if (result.IsSuccess)
            {
                return Ok(ContactMapper.Map(result.Value));
            }

            return StatusCodeMapper.Map(result.Status, result.Message);
        }
    }
}
