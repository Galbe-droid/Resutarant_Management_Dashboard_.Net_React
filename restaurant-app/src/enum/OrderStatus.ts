const OrderStatus = {
    Open:  0,
    Attention: 1,
    Finished: 2,
    Paid: 3,
    Cancelled: 4,
} as const;

type OrderStatus = typeof OrderStatus[keyof typeof OrderStatus];

export { OrderStatus };