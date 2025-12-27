using ChainSignal.Crypto.AI.Domain.Model;
using ChainSignal.Crypto.Core.Messages;
using FluentValidation;

namespace ChainSignal.Crypto.AI.API.Application.Commands.GenerateDailyReport
{
    public class GenerateDailyReportCommand : Command<MarketReport>
    {
        public GenerateDailyReportCommand() { }
        public override bool IsValid()
        {
            this.ValidationResult = new FluentValidation.Results.ValidationResult();
            return this.ValidationResult.IsValid;
        }
    }

    public class GenerateDailyReportCommandValidator : AbstractValidator<GenerateDailyReportCommand>
    {
        public GenerateDailyReportCommandValidator()
        {
        }
    }
}
