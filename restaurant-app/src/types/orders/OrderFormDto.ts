import type {OrderStatus} from "../../enum/OrderStatus.ts";

export interface OrderFormDto {
    tableId: string;
    tableNumber: number;
    orderStatus: OrderStatus;
}