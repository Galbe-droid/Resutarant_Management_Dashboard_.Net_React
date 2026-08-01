import {Box, Button, Divider, FormControl, InputLabel, MenuItem, Select, TextField} from "@mui/material";
import type {ReturnTableDto} from "../../types/tables/ReturnTableDto.ts";
import {TableStatus} from "../../enum/TableStatus.ts";
import {Controller, useForm, useWatch} from "react-hook-form";
import type {TableFormDto} from "../../types/tables/TableFormDto.ts";
import {useEffect} from "react";

interface TableFormProps {
    initialValues?: ReturnTableDto
    onSubmit: (data: TableFormDto) => void
    onCancelReservation?: () => void;
}

export function TableForm({initialValues, onSubmit, onCancelReservation}: TableFormProps) {
    const {register, control, handleSubmit, reset, formState: { errors }} = useForm<TableFormDto>({
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

    useEffect(() =>{
        if(!initialValues) return;

        reset({
            number: initialValues?.number,
            capacity: initialValues?.capacity,
            tableStatus: initialValues?.tableStatus,
            reservationName: initialValues?.reservationName,
            reservationTime: initialValues?.reservationTime ? new Date(initialValues?.reservationTime)
                    .toISOString()
                    .slice(0, 16)
                : null,
        })
    }, [initialValues, reset])

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
                    <Select
                        {...field}
                        labelId="table-status-label"
                        value={field.value}
                        label={"Estado"}
                        onChange={(e) =>
                            field.onChange(Number(e.target.value))
                    }>
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
            {initialValues && (
                <Button color="error" variant="outlined" onClick={onCancelReservation}>Cancelar Reserva</Button>
            )}
        </Box>
    )
}