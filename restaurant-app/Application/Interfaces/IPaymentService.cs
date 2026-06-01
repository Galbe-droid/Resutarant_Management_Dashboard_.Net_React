using Template_restaurant_app.Application.Common;
using Template_restaurant_app.Application.Dtos.Payment;

namespace Template_restaurant_app.Application.Interfaces
{
    public interface IPaymentService
    {
        public Task<Result<List<ReturnPaymentDto>>> GetAllAsync(Guid userId);
        public Task<Result<ReturnPaymentDto>> GetByIdAsync(Guid id, Guid userId);
        public Task<Result<ReturnPaymentDto>> CreateAsync(CreatePaymentDto create, Guid userId);
        public Task<Result<ReturnPaymentDto>> UpdateAsync(Guid id, UpdatePaymentDto update, Guid userId);
        public Task<Result<bool>> DeleteAsync(Guid id, Guid userId);
    }
}
