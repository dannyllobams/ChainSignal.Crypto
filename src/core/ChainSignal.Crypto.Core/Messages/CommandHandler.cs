using FluentValidation.Results;

namespace ChainSignal.Crypto.Core.Messages
{
    public abstract class CommandHandler
    {
        protected readonly ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }


        protected void AddError(string errorMessage)
        {
            var error = new ValidationFailure(string.Empty, errorMessage);
            ValidationResult.Errors.Add(error);
        }

        protected CommandResult<T> Result<T>(T? value = default)
        {
            return new CommandResult<T>(value, ValidationResult);
        }
    }
}
