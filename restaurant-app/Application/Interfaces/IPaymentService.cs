using Template_restaurant_app.Application.Dtos.Payment;

namespace Template_restaurant_app.Application.Interfaces
{
    public interface IPaymentService
    {
        public Task<List<ReturnPaymentDto>> GetAllAsync(Guid userId);
        public Task<ReturnPaymentDto> GetByIdAsync(Guid id, Guid userId);
        public Task<ReturnPaymentDto> CreateAsync(CreatePaymentDto create, Guid userId);
        public Task<ReturnPaymentDto> UpdateAsync(Guid id, UpdatePaymentDto update, Guid userId);
        public Task DeleteAsync(Guid id, Guid userId);
    }
}
