using MediatR;
 

namespace HexaPSA.Tool.Application.Abstractions.Messaging
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
