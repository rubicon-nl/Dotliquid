using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rubicon.Demo.Api.Controllers.V1.Mappers;
using Rubicon.Demo.Api.Domain;
using Rubicon.Demo.Api.Services.Interfaces;

namespace Rubicon.Demo.Api.Controllers.V1
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TemplateController : Controller
    {
        private readonly IContactService _contactService;
        private readonly ITemplateService _templateService;

        public TemplateController(IContactService contactService, ITemplateService templateService)
        {
            _contactService = contactService;
            _templateService = templateService;
        }

        [HttpGet("BasicTemplate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BasicTemplate()
        {
            Result<string> result = _templateService.RenderBasicTemplate();

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return StatusCodeMapper.Map(result.Status, result.Message);
        }

        [HttpGet("Contacts/{id:Guid}/Registration")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegistrationTemplate(Guid id)
        {
            Result<Contact> contactResult = await _contactService.GetAsync(id);

            if (!contactResult.IsSuccess)
            {
                return StatusCodeMapper.Map(contactResult.Status, contactResult.Message);
            }

            Result<string> templateResult = _templateService.RenderRegistrationTemplate(contactResult.Value);

            if (templateResult.IsSuccess)
            {
                return Ok(templateResult.Value);
            }

            return StatusCodeMapper.Map(templateResult.Status, templateResult.Message);
        }

        [HttpGet("Contacts/{id:Guid}/RegistrationWithBaseTemplate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegistrationWithBaseTemplate(Guid id)
        {
            Result<Contact> contactResult = await _contactService.GetAsync(id);

            if (!contactResult.IsSuccess)
            {
                return StatusCodeMapper.Map(contactResult.Status, contactResult.Message);
            }

            Result<string> templateResult = _templateService.RenderRegistrationWithBaseTemplate(contactResult.Value);

            if (templateResult.IsSuccess)
            {
                return Ok(templateResult.Value);
            }

            return StatusCodeMapper.Map(templateResult.Status, templateResult.Message);
        }
    }
}
