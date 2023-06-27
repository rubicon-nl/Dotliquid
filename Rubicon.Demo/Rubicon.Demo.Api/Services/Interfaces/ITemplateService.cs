using Rubicon.Demo.Api.Domain;

namespace Rubicon.Demo.Api.Services.Interfaces
{
    public interface ITemplateService
    {
        Result<string> RenderBasicTemplate();
        Result<string> RenderRegistrationTemplate(Domain.Contact contact);
        Result<string> RenderRegistrationWithBaseTemplate(Domain.Contact contact);
    }
}
