import type {OrderStatus} from "../../enum/OrderStatus.ts";

export interface UpdateOrderStatusChangeDto {
    orderStatus: OrderStatus;
}