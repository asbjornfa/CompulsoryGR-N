using FluentValidation;
using Service.DTO.Request;
using Service.DTO.Response;

namespace Service.Validators;

public class CreateOrderValidator : AbstractValidator<RequestCreateOrderDTO>
{
    public CreateOrderValidator()
    {
        RuleFor(o => o.OrderDate).NotEmpty().WithMessage("Order cannot be empty");
    }

}