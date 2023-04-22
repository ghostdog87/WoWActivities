using FluentValidation;

namespace Application.Profiles
{
    public class ProfileDtoValidator : AbstractValidator<ProfileDto>
    {
        public ProfileDtoValidator()
        {
            RuleFor(x => x.DisplayName).NotEmpty();
        }
    }
}
