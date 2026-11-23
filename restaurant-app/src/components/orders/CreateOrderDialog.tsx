import {Dialog, DialogContent, DialogTitle} from "@mui/material";
import {OrderForm} from "./OrderForm.tsx";
import type {ReturnAvailableTablesDto} from "../../types/tables/ReturnAvailableTablesDto.ts";
import {useSnackbar} from "../../hooks/useSnackbar.ts";
import type {OrderFormDto} from "../../types/orders/OrderFormDto.ts";
import type {CreateOrderDto} from "../../types/orders/CreateOrderDto.ts";
import {createOrder} from "../../services/OrderServices.tsx";
import {useTranslation} from "react-i18next";

interface CreateOrderDialogProps {
    open: boolean;
    onClose: () => void;
    onCreated: () => void;
    availableTables: ReturnAvailableTablesDto[]
}

export function CreateOrderDialog({open, onClose, onCreated, availableTables}: CreateOrderDialogProps) {
    const { t } = useTranslation("order");
    const { showSnackbar } = useSnackbar();

    const handleCreate = async (data: OrderFormDto) => {
        try {
            const dto: CreateOrderDto = {
                tableId: data.tableId,
            }
            await createOrder(dto);
            onCreated();
            showSnackbar("Sucesso ao criar pedido", "success");
            onClose();
        }catch (error){
            showSnackbar("Error ao criar pedido: " + error, "error");
        }
    }

    return(
        <Dialog open={open} onClose={onClose}>
            <DialogTitle>{t("titleCreateOrder")}</DialogTitle>

            <DialogContent>
                <OrderForm onSubmit={handleCreate} availableTables={availableTables}/>
            </DialogContent>
        </Dialog>
    )
}