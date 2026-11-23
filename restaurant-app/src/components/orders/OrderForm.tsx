import {Controller, useForm} from "react-hook-form";
import {Box, Button, Chip, FormControl, InputLabel, MenuItem, Select,} from "@mui/material";
import type {ReturnAvailableTablesDto} from "../../types/tables/ReturnAvailableTablesDto.ts";
import type {TableStatus} from "../../enum/TableStatus.ts";
import type {OrderFormDto} from "../../types/orders/OrderFormDto.ts";
import {useEffect} from "react";
import {useTranslation} from "react-i18next";

interface OrderFormProps {
    initialValues?: OrderFormDto;
    availableTables: ReturnAvailableTablesDto[];
    onSubmit: (data: OrderFormDto) => void;
    onDelete?: () => void;
}

export function OrderForm({initialValues, availableTables, onSubmit }: OrderFormProps) {
    const { t } = useTranslation("order");
    const {reset, handleSubmit, control,} = useForm<OrderFormDto>({
        defaultValues:{
            tableId: initialValues?.tableId ?? "",
        }
    });

    const getStatusChip = (status: TableStatus) => {
        switch (status) {
            case 0:
                return <Chip label={t("tableStatusAvaliable")} color="success"/>;

            default:
                return <Chip label={status} />;
        }
    }

    useEffect(() =>{
        if(!initialValues) return;

        reset({
            tableId: initialValues?.tableId,
        })
    }, [initialValues, reset])

    if(!initialValues){
        return(
            <Box component={"form"} onSubmit={handleSubmit(onSubmit)}
                 sx={{
                     display: "flex",
                     flexDirection: "column",
                     gap:3,
                     padding: 2
                 }}
            >
                <FormControl fullWidth>
                    <InputLabel id="table-status-label">Mesa Disponiveis</InputLabel>
                    <Controller name={"tableId"} control={control} render={({field}) => (
                        <Select
                            {...field}
                            labelId="table-status-label"
                            value={field.value}
                            label={"Mesas Disponiveis"}
                            onChange={(e) =>
                                field.onChange(e.target.value)
                            }>
                            {availableTables?.map((table) => (
                                <MenuItem value={table.id}>{table.number} | {getStatusChip(table.tableStatus)}</MenuItem>
                            ))}
                        </Select>
                    )}/>
                </FormControl>

                <Button type={"submit"}>{t("create")}</Button>
            </Box>
        )
    }
    else{
        return(
            <Box component={"form"} onSubmit={handleSubmit(onSubmit)}
                 sx={{
                     display: "flex",
                     flexDirection: "column",
                     gap:3,
                     padding: 2
                 }}
            >
                <FormControl fullWidth>
                    <InputLabel id="table-status-label">Estado</InputLabel>
                    <Controller name={"tableId"} control={control} render={({field}) => (
                        <Select
                            {...field}
                            labelId="table-status-label"
                            value={field.value ?? ""}
                            label="Mesas Disponíveis"
                            onChange={(e) => field.onChange(e.target.value)}
                        >
                            {/* Mesa atualmente associada ao pedido */}
                            <MenuItem value={initialValues.tableId} disabled>
                                Mesa {initialValues.tableNumber} (Atual)
                            </MenuItem>

                            {/* Outras mesas disponíveis */}
                            {availableTables?.map((table) => (
                                <MenuItem
                                    key={table.id}
                                    value={table.id}
                                >
                                    {table.number} | {getStatusChip(table.tableStatus)}
                                </MenuItem>
                            ))}
                        </Select>
                    )}/>
                </FormControl>

                <Button type={"submit"}>{t("update")}</Button>
            </Box>
        )
    }
}