import type {OrderStatus} from "../../enum/OrderStatus.ts";
import type ReturnOrderItemDto from "../ordersItens/ReturnOrderItemDto.ts";

export interface ReturnOrderDto{
    id: string;
    tableId: string;
    tableNumber: number;
    orderDate: string;
    totalAmount: number;
    orderStatus: OrderStatus;
    orderItems: ReturnOrderItemDto[];
}