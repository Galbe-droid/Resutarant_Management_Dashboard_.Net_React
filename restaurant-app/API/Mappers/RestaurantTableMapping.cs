using Template_restaurant_app.Application.Dtos.Table;
using Template_restaurant_app.Domain.Entities;

namespace Template_restaurant_app.API.Mappers
{
    public static class RestaurantTableMapping
    {
        public static RestaurantTable ToRestaurantTable(CreateTableDto create)
        {
            return new RestaurantTable
            {
                Number = create.Number,
                Capacity = create.Capacity,
            };
        }

        public static RestaurantTable ToRestaurantTable(RestaurantTable table, ChangeTableStatusDto change)
        {
            table.TableStatus = change.Status;

            if(change.ReservationName != null)
            {
                table.ReservationName = change.ReservationName;
            }

            if (change.ReservationTime != null)
            {
                table.ReservationTime = change.ReservationTime;
            }

            return table;
        }

        public static RestaurantTable ToRestaurantTable(RestaurantTable table, UpdateTableDto update)
        {
            table.Number = update.Number;
            table.Capacity = update.Capacity;

            return table;
        }

        public static ReturnTableDto ToReturnTableDto(RestaurantTable table)
        {
            return new ReturnTableDto
            {
                Id = table.Id,
                Number = table.Number,
                Capacity = table.Capacity,
                Orders = OrderMapping.ToReturnOrders(table.Orders.ToList()),
                TableStatus = (int)table.TableStatus,
                ReservationName = table.ReservationName,
                ReservationTime = table.ReservationTime,
            };
        }

        public static List<ReturnTableDto> ToReturnTables(List<RestaurantTable> list)
        {
            return (from RestaurantTable table in list select ToReturnTableDto(table)).ToList();
        }
    }
}
