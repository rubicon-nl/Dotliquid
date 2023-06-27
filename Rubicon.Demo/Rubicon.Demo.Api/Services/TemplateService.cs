using Rubicon.Demo.Api.Domain;
using Rubicon.Demo.Api.Services.Interfaces;
using Rubicon.Demo.Api.Services.Models;

namespace Rubicon.Demo.Api.Services
{
    public class TemplateService : ITemplateService
    {
        public TemplateService()
        {
            // Configure DotLiquid to use the local file system for include and extends tags.
            string templateFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Services\\Templates");
            DotLiquid.Template.FileSystem = new DotLiquid.FileSystems.LocalFileSystem(templateFolderPath);
        }

        public Result<string> RenderBasicTemplate()
        {
            string templateContent = "Hi {{name}}";

            DotLiquid.Template template = DotLiquid.Template.Parse(templateContent);

            DotLiquid.Hash localVariables = DotLiquid.Hash.FromAnonymousObject(new { name = "Rubicon" });

            string result = template.Render(localVariables);

            return new Result<string>(result);
        }

        public Result<string> RenderRegistrationTemplate(Domain.Contact contact)
        {
            string templateContent = File.ReadAllText("Services/Templates/ContactRegistartionTemplate.liquid");

            DotLiquid.Template template = DotLiquid.Template.Parse(templateContent);

            DotLiquid.Hash localVariables = DotLiquid.Hash.FromAnonymousObject(new
            {
                Contact = new ContactDrop(contact),
                DateTime = DateTime.Now,
            });

            string result = template.Render(localVariables);

            return new Result<string>(result);
        }

        public Result<string> RenderRegistrationWithBaseTemplate(Domain.Contact contact)
        {
            try
            {
                string templateContent = File.ReadAllText("Services/Templates/ContactRegistartionWithBaseTemplate.liquid");

                DotLiquid.Template template = DotLiquid.Template.Parse(templateContent);

                DotLiquid.Hash localVariables = DotLiquid.Hash.FromAnonymousObject(new
                {
                    Contact = new ContactDrop(contact),
                    DateTime = DateTime.Now,
                });

                string result = template.Render(localVariables);

                if (template.Errors.Any())
                {
                    IEnumerable<string> errorMessages = template.Errors.Select(error => error.Message);

                    return new Result<string>(Domain.Enums.ResultStatus.Error, string.Join(", ", errorMessages));
                }

                return new Result<string>(result);
            }
            catch (Exception ex)
            {
                return new Result<string>(Domain.Enums.ResultStatus.Error, ex.Message);
            }
        }
    }
}
