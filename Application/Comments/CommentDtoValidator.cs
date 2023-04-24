using Application.Activities;
using Domain;
using FluentValidation;

namespace Application.Comments
{
    public class CommentDtoValidator : AbstractValidator<Comment>
    {
        public CommentDtoValidator()
        {
            RuleFor(x => x.Body).NotEmpty();
        }

    }
}
