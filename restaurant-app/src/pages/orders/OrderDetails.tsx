import {Button, CircularProgress} from "@mui/material";
import {useNavigate, useParams} from "react-router-dom";
import {useSnackbar} from "../../hooks/useSnackbar.ts";
import {useEffect, useState} from "react";
import type {ReturnOrderDto} from "../../types/orders/ReturnOrderDto.ts";
import type {ReturnAvailableTablesDto} from "../../types/tables/ReturnAvailableTablesDto.ts";
import {
    addOrderItem,
    deleteOrder, deleteOrderItem,
    getOrder, updateOrderItem,
    updateOrderStatus,
    updateOrderTable
} from "../../services/OrderServices.tsx";
import {OrderForm} from "../../components/orders/OrderForm.tsx";
import {getAllAvailableTables} from "../../services/TableServices.tsx";
import type {OrderFormDto} from "../../types/orders/OrderFormDto.ts";
import type {UpdateOrderStatusChangeDto} from "../../types/orders/UpdateOrderStatusChangeDto.ts";
import type {UpdateOrderTableDto} from "../../types/orders/UpdateOrderTableDto.ts";
import {OrderItemList} from "../../components/orderItemList/OrderItemList.tsx";
import type ReceiveOrderItemDto from "../../types/ordersItens/ReceiveOrderItemDto.ts";
import {AddOrderItemDialog} from "../../components/orderItemList/AddOrderItemDialog.tsx";
import type ReturnOrderItemDto from "../../types/ordersItens/ReturnOrderItemDto.ts";
import {UpdateOrderItemDialog} from "../../components/orderItemList/UpdateOrderItemDialog.tsx";
import {OrderStatus} from "../../enum/OrderStatus.ts";

export function OrderDetails() {
    const { id } = useParams();
    const { showSnackbar } = useSnackbar();
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);
    const [order, setOrder] = useState<ReturnOrderDto | null>();
    const isOrderOpen = order?.orderStatus === OrderStatus.Open;
    const [avaliableTables, setAvailableTables] = useState<ReturnAvailableTablesDto[] | null>(null);
    const [openAddItem, setOpenAddItem] = useState(false);
    const [openUpdateItem, setOpenUpdateItem] = useState(false);
    const [selectedItem, setSelectedItem] = useState<ReturnOrderItemDto | null>(null);

    const loadOrder = async() => {
        if(!id) return;

        try{
            const data = await getOrder(id);
            setOrder(data);
        }finally {
            setLoading(false);
        }
    }

    const loadAvailableTables = async () => {
        try{
            const response = await getAllAvailableTables();
            setAvailableTables(response);
        }
        catch(error){
            console.log(error);
        }
    }

    const handleAddItem = async (data: ReceiveOrderItemDto) => {
        try{
            const updatedOrder = await addOrderItem(id!, data)

            setOrder(updatedOrder);
            setOpenAddItem(false);

            showSnackbar("Produto adicionado com sucesso!", "success");
        } catch (error) {
            showSnackbar("Erro ao adicionar produto: " + error, "error");
        }
    }

    const handleUpdate = async(data: OrderFormDto) => {
        try{
            if(order!.tableId != data.tableId)
            {
                const dto: UpdateOrderTableDto = {
                    tableId: data.tableId,
                }
                await updateOrderTable(id!, dto)
            }

            if(order!.orderStatus != data.orderStatus)
            {
                const dto: UpdateOrderStatusChangeDto = {
                    orderStatus: data.orderStatus,
                }
                await updateOrderStatus(id!, dto)
            }
            showSnackbar( "Sucesso", "success");
            navigate("orders")
        }catch(error){
            showSnackbar( "Erro ao atualizar pedido: " + error, "error");
        }
    }

    const handleUpdateItem = async (
        itemId: string,
        quantity: number
    ) => {
        try {
            const updatedOrder = await updateOrderItem(
                id!,
                itemId,
                quantity
            );

            setOrder(updatedOrder);
            showSnackbar("Order item updated successfully!", "success");
        } catch (error) {
            showSnackbar("Error updating order item.", "error");

            throw error;
        }
    };

    const handleDeleteItem = async (item: ReturnOrderItemDto) => {
        try {
            const updatedOrder = await deleteOrderItem(id!, item.id);

            if (!updatedOrder) {
                showSnackbar("Could not remove order item.", "error");
                return;
            }

            await loadOrder();

            showSnackbar("Order item removed successfully!", "success");
        } catch (error) {
            showSnackbar("Error removing order item." + error, "error");
        }
    };

    const handleDelete = async() => {
        try{
            await deleteOrder(id!);
            showSnackbar( "Sucesso", "success");
            navigate("orders")
        }catch(error){
            showSnackbar( "Erro ao deletar pedido: " + error, "error");
        }
    }

    useEffect(() => {
        const load = async () => {
            await Promise.all([
                loadOrder(),
                loadAvailableTables()
            ]);
        };

        load();
    }, []);

    if(loading){
        return <CircularProgress/>
    }

    return(
        <>
            {order && (
                <>
                    <OrderItemList
                        items={order!.orderItems}
                        totalAmount={order!.totalAmount}
                        onEdit={(item) => {
                            setSelectedItem(item);
                            setOpenUpdateItem(true);
                        }}
                        onDelete={handleDeleteItem}
                        canEdit={isOrderOpen}
                    />

                    <UpdateOrderItemDialog
                        open={openUpdateItem}
                        item={selectedItem}
                        onClose={() => {
                            setOpenUpdateItem(false);
                            setSelectedItem(null);
                        }}
                        onSubmit={handleUpdateItem}
                    />

                    {isOrderOpen && (
                        <>
                            <Button
                                variant="contained"
                                onClick={() => setOpenAddItem(true)}
                            >
                                Adicionar produto
                            </Button>

                            <AddOrderItemDialog
                                open={openAddItem}
                                onClose={() => setOpenAddItem(false)}
                                onSubmit={handleAddItem}
                            />
                        </>
                    )}

                    <OrderForm initialValues={order!} availableTables={avaliableTables!} onSubmit={handleUpdate} onDelete={handleDelete}/>
                </>
            )}
        </>
    )
}