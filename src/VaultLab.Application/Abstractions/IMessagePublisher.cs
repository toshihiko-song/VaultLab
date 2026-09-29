
namespace VaultLab.Application.Abstractions
{
    public interface IMessagePublisher
    {
        Task PublishMessage<T>(
            T message,
            CancellationToken cancellationToken = default
        );
    }
}