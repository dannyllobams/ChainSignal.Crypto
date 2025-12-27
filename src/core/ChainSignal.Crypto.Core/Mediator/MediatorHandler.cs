using ChainSignal.Crypto.Core.Messages;
using Cortex.Mediator;
using Cortex.Mediator.Commands;
using FluentValidation.Results;

namespace ChainSignal.Crypto.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ValidationResult> CallCommand<T>(T command, CancellationToken cancelationToken = default) where T : Command
        {
            return await _mediator.SendCommandAsync<T, ValidationResult>(command, cancelationToken);
        }

        public async Task<CommandResult<TResult>> CallCommand<T, TResult>(T command, CancellationToken cancelationToken = default)
            where T : Command<TResult>
        {
            return await _mediator.SendCommandAsync<T, CommandResult<TResult>>(command, cancelationToken);
        }
    }
}
