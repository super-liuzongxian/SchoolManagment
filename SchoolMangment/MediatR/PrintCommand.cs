using MediatR;

namespace SchoolMangment.MediatR
{
    public class PrintCommand:IRequest<int>
    {
        public string CommandName { get; set; }
    }
}
