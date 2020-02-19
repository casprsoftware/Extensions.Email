using System;

namespace CASPR.Extensions.Email.Exceptions
{
    public class EmailTemplateNotFoundException : Exception
    {
        public EmailTemplateNotFoundException(string templateName) 
            : base($"The template with name '{templateName}' not found.")
        {
            TemplateName = templateName;
        }

        public string TemplateName { get; }
    }
}
