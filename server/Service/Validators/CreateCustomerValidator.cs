using FluentValidation;
using Service.DTO.Request;
using Service.DTO.Response;

namespace Service.Validators;

public class CreateCustomerValidator : AbstractValidator<RequestCreateCustomerDTO>
{
    public CreateCustomerValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Name cannot be empty");
    }
}