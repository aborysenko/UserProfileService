using FluentValidation;
using UserProfileService.Domain.Query;

namespace UserProfileService.Domain.Validation
{
    public class GetUserQueryValidator : AbstractValidator<GetUsersQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(x => x.PageSize).GreaterThan(0);
            RuleFor(x => x.CurrentPage).GreaterThanOrEqualTo(0);
        }
    }
}
