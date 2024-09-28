using FluentValidation;
using Service.DTO.Request;

namespace Service.Validators;

public class CreatePropertyValidator : AbstractValidator<RequestCreatePropertyDTO>
{
    public CreatePropertyValidator() 
    {
        RuleFor(p => p.PropertyName).NotEmpty().WithMessage("Property name cannot be empty");
    }
}