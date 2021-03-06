﻿using System.Threading.Tasks;
using CASPR.Extensions.Email.Models;

namespace CASPR.Extensions.Email
{
    public class DefaultEmailBuilder : EmailBuilder
    {
        #region Private Declarations
        private readonly IEmailSender _emailSender;
        #endregion

        #region Constructor

        public DefaultEmailBuilder(
            IEmailSender emailSender,
            IEmailTemplateEngine templateEngine, 
            IEmailTemplateStorage emailTemplateStorage,
            EmailAddress defaultFrom) 
            : base(templateEngine, emailTemplateStorage, defaultFrom)
        {
            _emailSender = emailSender;
        }

        #endregion

        #region Send Message
        public override void Send()
        {
            SendAsync()
                .GetAwaiter()
                .GetResult();
        }

        public override async Task SendAsync()
        {
            await _emailSender.SendAsync(Message);
        }
        #endregion
    }
}
