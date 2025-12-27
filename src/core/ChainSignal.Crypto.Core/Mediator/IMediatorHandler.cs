using ChainSignal.Crypto.Core.Messages;
using FluentValidation.Results;

namespace ChainSignal.Crypto.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task<ValidationResult> CallCommand<T>(T command, CancellationToken cancelationToken = default) where T : Command;

        Task<CommandResult<TResult>> CallCommand<T, TResult>(T command, CancellationToken cancelationToken = default)
            where T : Command<TResult>;
    }
}
