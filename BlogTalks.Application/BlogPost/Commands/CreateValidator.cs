using FluentValidation;

namespace BlogTalks.Application.BlogPost.Commands
{
    public class CreateValidator : AbstractValidator<CreateRequest>
    {
        public CreateValidator()
        {
            RuleFor(bp => bp.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            RuleFor(bp => bp.Text)
                .NotEmpty().WithMessage("Text is required.")
                .MaximumLength(5000).WithMessage("Content must not exceed 5000 characters.");

            RuleFor(bp => bp.Tags)
                .NotEmpty().WithMessage("At least one tag is required.")
                .ForEach(tag => tag
                    .NotEmpty().WithMessage("Tag cannot be empty."));
        }
    }
}
