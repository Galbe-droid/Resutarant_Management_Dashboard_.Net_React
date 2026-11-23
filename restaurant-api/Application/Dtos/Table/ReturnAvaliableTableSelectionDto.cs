using Template_restaurant_app.Domain.Enum;

namespace Template_restaurant_app.Application.Dtos.Table
{
    public class ReturnAvaliableTableSelectionDto
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public TableStatus TableStatus { get; set; }
    }
}
