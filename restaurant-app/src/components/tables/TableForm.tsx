import {Box, Button, Divider, FormControl, InputLabel, MenuItem, Select, TextField} from "@mui/material";
import type {ReturnTableDto} from "../../types/tables/ReturnTableDto.ts";
import {TableStatus} from "../../enum/TableStatus.ts";
import {Controller, useForm, useWatch} from "react-hook-form";
import type {TableFormDto} from "../../types/tables/TableFormDto.ts";
import {useEffect} from "react";
import {useTranslation} from "react-i18next";

interface TableFormProps {
    initialValues?: ReturnTableDto
    onSubmit: (data: TableFormDto) => void
    onCancelReservation?: () => void;
    onDeleteTable?: () => void;
}

export function TableForm({initialValues, onSubmit, onCancelReservation, onDeleteTable}: TableFormProps) {
    const { t } = useTranslation("table");
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
            <TextField fullWidth label={t("field1")} type={"number"} {...register("number")}
                        error={!!errors.number} helperText={errors.number?.message}/>

            <TextField fullWidth label={t("field2")} type={"number"} {...register("capacity")}
                       error={!!errors.capacity} helperText={errors.capacity?.message}/>

            <FormControl fullWidth>
                <InputLabel id="table-status-label">Estado</InputLabel>
                <Controller name={"tableStatus"} control={control} render={({field}) => (
                    <Select
                        {...field}
                        labelId="table-status-label"
                        value={field.value}
                        label={t("field3")}
                        onChange={(e) =>
                            field.onChange(Number(e.target.value))
                    }>
                        <MenuItem value={TableStatus.Avaliable}>{t("statusAvaliable")}</MenuItem>
                        <MenuItem value={TableStatus.Occupied}>{t("statusOccupied")}</MenuItem>
                        <MenuItem value={TableStatus.Reserved}>{t("statusReserved")}</MenuItem>
                    </Select>
                )}/>
            </FormControl>

            {tableStatus === TableStatus.Reserved ?
                <>
                    <Divider/>
                    <TextField fullWidth label={t("field4")} type={"text"} {...register("reservationName")}/>
                    <TextField fullWidth label={t("field5")} type={"datetime-local"} {...register("reservationTime")}/>
                </>
            :
                <></>
            }
            <Button type={"submit"}>{initialValues ? t("buttonEdit") : t("bottonNew")}</Button>
            {initialValues && (
                <>
                    <Button color="error" variant="outlined" onClick={onCancelReservation}>{t("bottonCancelReserve")}</Button>
                    <Button color="error" variant="outlined" onClick={onDeleteTable}>{t("buttonDelete")}</Button>
                </>
            )}
        </Box>
    )
}