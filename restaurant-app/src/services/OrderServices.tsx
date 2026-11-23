import type {ReturnOrderDto} from "../types/orders/ReturnOrderDto.ts";
import {api, type ApiResponse} from "../api/axios.ts";
import type {CreateOrderDto} from "../types/orders/CreateOrderDto.ts";
import type {UpdateOrderTableDto} from "../types/orders/UpdateOrderTableDto.ts";
import type {UpdateOrderStatusChangeDto} from "../types/orders/UpdateOrderStatusChangeDto.ts";
import type ReceiveOrderItemDto from "../types/ordersItens/ReceiveOrderItemDto.ts";

export const getAllOrders = async (): Promise<ReturnOrderDto[]> => {
    const response = await api.get<ApiResponse<ReturnOrderDto[]>>("/orders");
    return response.data.data;
}

export const getOrder = async (id: string): Promise<ReturnOrderDto> => {
    const response = await api.get<ApiResponse<ReturnOrderDto>>(`/orders/${id}`);
    return response.data.data;
}

export const createOrder = async (data: CreateOrderDto): Promise<ReturnOrderDto> => {
    const response = await api.post<ApiResponse<ReturnOrderDto>>(`/orders/create`, data);
    return response.data.data;
}

export const updateOrderTable = async(id: string, data: UpdateOrderTableDto): Promise<ReturnOrderDto> => {
    const response = await api.put<ApiResponse<ReturnOrderDto>>(`/orders/updateTable/${id}`, data);
    return response.data.data;
}

export const updateOrderStatus = async(id: string, data: UpdateOrderStatusChangeDto): Promise<ReturnOrderDto> => {
    const response = await api.put<ApiResponse<ReturnOrderDto>>(`/orders/updateStatus/${id}`, data);
    return response.data.data;
}

export const deleteOrder = async (id: string): Promise<boolean> => {
    const response = await api.delete<ApiResponse<boolean>>(`/orders/delete/${id}`);
    return response.data.data;
}

export const addOrderItem = async(id: string, data: ReceiveOrderItemDto): Promise<ReturnOrderDto> => {
    const response = await api.post<ApiResponse<ReturnOrderDto>>(`/orders/addOrderItems/${id}/items/`, data);
    return response.data.data;
}

export const updateOrderItem = async(id: string, orderItemId: string, quantity: number): Promise<ReturnOrderDto> => {
    const response = await api.put<ApiResponse<ReturnOrderDto>>(`/orders/UpdateOrderItems/${id}/items/${orderItemId}`,
        JSON.stringify(quantity),
        {
            headers: {
                "Content-Type": "application/json"
            }
        }
    );
    return response.data.data;
}

export const deleteOrderItem = async(id: string, orderItemId: string): Promise<boolean> => {
    const response = await api.delete<ApiResponse<boolean>>(`/orders/removeOrderItems/${id}/items/${orderItemId}`);
    return response.data.data;
}