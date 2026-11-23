export default interface ReturnOrderItemDto {
    id: string,
    orderId: string,
    productId: string,
    productName: string,
    quantity: number,
    totalPrice: number
}