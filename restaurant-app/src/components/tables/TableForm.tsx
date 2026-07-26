import {Box, Button, Divider, FormControl, InputLabel, MenuItem, Select, TextField} from "@mui/material";
import type {ReturnTableDto} from "../../types/tables/ReturnTableDto.ts";
import {TableStatus} from "../../enum/TableStatus.ts";
import {Controller, useForm, useWatch} from "react-hook-form";
import type {TableFormDto} from "../../types/tables/TableFormDto.ts";

interface TableFormProps {
    initialValues?: ReturnTableDto
    onSubmit: (data: TableFormDto) => void
}

export function TableForm({initialValues, onSubmit}: TableFormProps) {
    const {register, control, handleSubmit, formState: { errors }} = useForm<TableFormDto>({
        defaultValues:{
            number: initialValues?.number ?? 1,
            capacity: initialValues?.capacity ?? 4,
            tableStatus: initialValues?.tableStatus ?? TableStatus.Avaliable,
            reservationName: initialValues?.reservationName ?? "",
            reservationTime: initialValues?.reservationTime ?? null
        }
    });
    const tableStatus = useWatch({
        control,
        name: "tableStatus",
    })

    return (
        <Box component={"form"} onSubmit={handleSubmit(onSubmit)}
            sx={{
                display: "flex",
                flexDirection: "column",
                gap:3,
                padding: 2
            }}
        >
            <TextField fullWidth label={"Numero da mesa"} type={"number"} {...register("number")}
                        error={!!errors.number} helperText={errors.number?.message}/>

            <TextField fullWidth label={"Capacidade"} type={"number"} {...register("capacity")}
                       error={!!errors.capacity} helperText={errors.capacity?.message}/>

            <FormControl fullWidth>
                <InputLabel id="table-status-label">Estado</InputLabel>
                <Controller name={"tableStatus"} control={control} render={({field}) => (
                    <Select {...field}  labelId="table-status-label" label={"Estado"}>
                        <MenuItem value={TableStatus.Avaliable}>Avaliable</MenuItem>
                        <MenuItem value={TableStatus.Occupied}>Occupied</MenuItem>
                        <MenuItem value={TableStatus.Reserved}>Reserved</MenuItem>
                    </Select>
                )}/>
            </FormControl>

            {tableStatus === TableStatus.Reserved ?
                <>
                    <Divider/>
                    <TextField fullWidth label={"Nome da Reserva"} type={"text"} {...register("reservationName")}/>
                    <TextField fullWidth label={"Horario da Reserva "} type={"datetime-local"} {...register("reservationTime")}/>
                </>
            :
                <></>
            }
            <Button type={"submit"}>{initialValues ? "Editar Mesa" : "Nova Mesa"}</Button>
        </Box>
    )
}