using Template_restaurant_app.Application.Dtos.Payment;
using Template_restaurant_app.Domain.Entities;

namespace Template_restaurant_app.API.Mappers
{
    public static class PaymentMapping
    {
        public static Payment ToPayment(CreatePaymentDto create, Order order)
        {
            return new Payment
            {
                OrderId = create.OrderId,
                Order = order,
                Amount = order.TotalAmount,
                PaymentMethod = create.PaymentMethod
            };
        }
        public static Payment ToPayment(UpdatePaymentDto update, Order order, Payment payment)
        {
            if(update.OrderId != payment.OrderId)
            {
                payment.OrderId = update.OrderId;
                payment.Amount = order.TotalAmount;
                payment.Order = order;
            }
            
            payment.PaymentMethod = update.PaymentMethod;
            return payment;
        }
        public static ReturnPaymentDto ToReturnPaymentDto(Payment payment)
        {
            return new ReturnPaymentDto
            {
                Id = payment.Id,
                OrderId = payment.OrderId,
                Order = OrderMapping.ToReturnOrder(payment.Order),
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod
            };
        }

        public static List<ReturnPaymentDto> ToReturnPaymentDtoList(List<Payment> payments)
        {
            var returnPayments = new List<ReturnPaymentDto>();
            foreach (var payment in payments)
            {
                returnPayments.Add(ToReturnPaymentDto(payment));
            }
            return returnPayments;
        }
    }
}
