using FluentValidation.Results;

namespace ChainSignal.Crypto.Core.Messages
{
    public class CommandResult<T>
    {
        public T? Value { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public CommandResult(ValidationResult validationResult)
        {
            this.ValidationResult = validationResult;
        }

        public CommandResult(T? value, ValidationResult validationRestult)
        {
            this.Value = value;
            this.ValidationResult = validationRestult;
        }
    }
}
