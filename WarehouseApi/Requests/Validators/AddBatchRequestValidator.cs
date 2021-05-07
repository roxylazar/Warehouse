using FluentValidation;
using WarehouseBusiness.Services;

namespace WarehouseApi.Requests.Validators
{
    public class AddBatchRequestValidator : AbstractValidator<AddBatchRequest>
    {
        public AddBatchRequestValidator(IClockService clock)
        {
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.Quantity).NotEmpty();

            RuleFor(x => x.ExpirationDate).NotEmpty();
            RuleFor(x => x.ExpirationDate).GreaterThanOrEqualTo(clock.Now.Date.AddDays(1))
                .WithMessage("Expiration date should begin from tomorrow.");

            RuleFor(x => x.ProductName).NotEmpty();
        }
    }
}