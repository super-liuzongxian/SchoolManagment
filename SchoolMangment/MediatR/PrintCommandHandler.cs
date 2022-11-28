using MediatR;

namespace SchoolMangment.MediatR
{
    public class PrintCommandHandler : IRequestHandler<PrintCommand,int>
    {
        public Task<int> Handle(PrintCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("请求过来的命令"+ request.CommandName);
            return Task.FromResult(1);
        }
    }
}
