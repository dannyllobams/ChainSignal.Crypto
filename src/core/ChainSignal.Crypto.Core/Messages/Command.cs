using Cortex.Mediator.Commands;
using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace ChainSignal.Crypto.Core.Messages
{
    public abstract class Command : ICommand<ValidationResult>
    {
        public DateTime Timestamp { get; private set; }

        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; } = new ValidationResult();

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }

    public abstract class Command<TResult> : Command, ICommand<CommandResult<TResult>>
    {
        protected Command()
        {

        }
    }
}
