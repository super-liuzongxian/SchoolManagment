using MediatR;

namespace SchoolMangment.MediatR
{
    public class Email:IRequest<long>
    {
        public string Address { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
