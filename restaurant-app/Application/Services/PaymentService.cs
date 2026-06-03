using Microsoft.EntityFrameworkCore;
using Template_restaurant_app.API.Mappers;
using Template_restaurant_app.Application.Common;
using Template_restaurant_app.Application.Dtos.Payment;
using Template_restaurant_app.Application.Interfaces;
using Template_restaurant_app.Domain.Enum;
using Template_restaurant_app.Repository;

namespace Template_restaurant_app.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly RestaurantDbContext _context;
        public PaymentService(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<ReturnPaymentDto>>> GetAllAsync(Guid userId)
        {
            _context.CurrentUserId = userId;

            var payments = await _context.Payments.AsNoTracking().ToListAsync();

            return Result<List<ReturnPaymentDto>>.Ok(PaymentMapping.ToReturnPaymentDtoList(payments));
        }

        public async Task<Result<ReturnPaymentDto>> GetByIdAsync(Guid id, Guid userId)
        {
            _context.CurrentUserId = userId;

            var payment = await _context.Payments.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);  

            if (payment == null)
            {
                return Result<ReturnPaymentDto>.Fail("Payment not found");
            }

            return Result<ReturnPaymentDto>.Ok(PaymentMapping.ToReturnPaymentDto(payment));
        }

        public async Task<Result<ReturnPaymentDto>> CreateAsync(CreatePaymentDto create, Guid userId)
        {
            _context.CurrentUserId = userId;

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == create.OrderId);

            if (order == null)
            {
                return Result<ReturnPaymentDto>.Fail("Order not found");
            }

            if(order.Status == OrderStatus.Paid || order.Status == OrderStatus.Finished || order.Status == OrderStatus.Cancelled)
            {
                return Result<ReturnPaymentDto>.Fail("Order cannot be paid");
            }

            order.Status = OrderStatus.Paid;
            var payment = PaymentMapping.ToPayment(create, order);

            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();

            return Result<ReturnPaymentDto>.Ok(PaymentMapping.ToReturnPaymentDto(payment));
        }

        public async Task<Result<ReturnPaymentDto>> UpdateAsync(Guid id, UpdatePaymentDto update, Guid userId)
        {
            _context.CurrentUserId = userId;

            var order = _context.Orders.FirstOrDefault(o => o.Id == update.OrderId);
            
            if (order == null)
            {
                return Result<ReturnPaymentDto>.Fail("Order not found");
            }

            if (order.Status == OrderStatus.Paid || order.Status == OrderStatus.Finished || order.Status == OrderStatus.Cancelled)
            {
                return Result<ReturnPaymentDto>.Fail("Order cannot be paid");
            }

            var payment = _context.Payments.FirstOrDefault(p => p.Id == id);

            if (payment == null)
            {
                return Result<ReturnPaymentDto>.Fail("Payment not found");
            }

            payment = PaymentMapping.ToPayment(update, order, payment);
            await _context.SaveChangesAsync();

            return Result<ReturnPaymentDto>.Ok(PaymentMapping.ToReturnPaymentDto(payment));
        }

        public async Task<Result<bool>> DeleteAsync(Guid id, Guid userId)
        {
            _context.CurrentUserId = userId;

            var payment = _context.Payments.FirstOrDefault(p => p.Id == id);

            if (payment == null)
            {
                return Result<bool>.Fail("Payment not found");
            }

            payment.IsDeleted = true;
            await _context.SaveChangesAsync();
            return Result<bool>.Ok(true);
        }
    }
}
