using System.Text.RegularExpressions;
using FluentValidation;
using UserProfileService.Domain.Commands;

namespace UserProfileService.Domain.Validation
{
    public class UserCommandBaseValidator<TCommand> : AbstractValidator<TCommand> 
        where TCommand : UserCommandBase
    {
        public UserCommandBaseValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Phone).Matches(new Regex("\\(?\\d{3}\\)?-? *\\d{3}-? *-?\\d{4}"))
                .WithMessage("Incorrect phone number.");
        }
    }
}
