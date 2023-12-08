namespace HexaPSA.Tool.Application.Abstractions.Messaging
{
    public interface IIdempotentCommand<out TResponse> : ICommand<TResponse>
    {
        Guid RequestId { get; set; }
    }
}
