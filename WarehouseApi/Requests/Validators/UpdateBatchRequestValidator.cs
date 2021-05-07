using FluentValidation;

namespace WarehouseApi.Requests.Validators
{
    public class UpdateBatchRequestValidator : AbstractValidator<UpdateBatchRequest>
    {
        public UpdateBatchRequestValidator()
        {
            RuleFor(x => x.DeliveredQuantity).GreaterThan(0).When(x => x.AddedQuantity == null || x.AddedQuantity == 0);
            RuleFor(x => x.AddedQuantity).GreaterThan(0).When(x => x.DeliveredQuantity == null || x.DeliveredQuantity == 0);
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}