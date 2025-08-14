using FluentValidation;

namespace BlogTalks.Application.Comment.Commands
{
    public class CreateValidator : AbstractValidator<CreateRequest>
    {
        public CreateValidator()
        {
            RuleFor(c => c.Text)
                .NotEmpty().WithMessage("Comment text is required.")
                .MaximumLength(500).WithMessage("Comment text must not exceed 500 characters.");
        }
    }
}
