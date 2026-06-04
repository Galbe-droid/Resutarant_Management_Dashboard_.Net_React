using Template_restaurant_app.Application.Common;
using Template_restaurant_app.Application.Dtos.Table;

namespace Template_restaurant_app.Application.Interfaces
{
    public interface IRestaurantTableService
    {
        public Task<Result<List<ReturnTableDto>>> GetAllAsync(Guid userId);
        public Task<Result<ReturnTableDto>> GetByIdAsync(Guid id, Guid userId);
        public Task<Result<ReturnTableDto>> CreateAsync(CreateTableDto create, Guid userId);
        public Task<Result<ReturnTableDto>> UpdateAsync(Guid id, UpdateTableDto update, Guid userId);
        public Task<Result<ReturnTableDto>> ReservationAsync(Guid id, Guid userId, ChangeTableStatusDto change);
        public Task<Result<ReturnTableDto>> CancelReservationAsync(Guid id, Guid userId);
        public Task<Result<bool>> DeleteAsync(Guid id, Guid userId);
    }
}
