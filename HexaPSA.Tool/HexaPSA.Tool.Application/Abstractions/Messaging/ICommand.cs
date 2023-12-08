using MediatR;
 
namespace HexaPSA.Tool.Application.Abstractions.Messaging
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
