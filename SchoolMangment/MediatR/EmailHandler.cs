using MediatR;
using Microsoft.Extensions.Options;
using SchoolMangment.Dtos;
using SchoolMangment.Utils;

namespace SchoolMangment.MediatR
{
    public class EmailHandler : IRequestHandler<Email, long>
    {
        private readonly AbstractSendEmailHelper sendEmailHelper;
        private readonly IOptions<EmailDto> options;
        public EmailHandler(AbstractSendEmailHelper sendEmailHelper, IOptions<EmailDto> options)
        {
            this.sendEmailHelper = sendEmailHelper;
            this.options = options;
        }
        public Task<long> Handle(Email request, CancellationToken cancellationToken)
        {
            Console.WriteLine("发送邮件给"+request.Address);
            //sendEmailHelper.Body = request.Body;
            //sendEmailHelper.MailTo = request.Address;
            //sendEmailHelper.MailFrom = options.Value.MailFrom;
            //sendEmailHelper.Subject =request.Title;
            //sendEmailHelper.Token = options.Value.Token;
            //sendEmailHelper.Send();
            return Task.FromResult(10L);
        }
    }
}
