import type {TableFormDto} from "../../types/tables/TableFormDto.ts";
import type {UpdateTableDto} from "../../types/tables/UpdateTableDto.ts";
import {
    cancelReservationTable,
    getTable,
    reservationUpdateTable,
    statusUpdateTable,
    updateTable
} from "../../services/TableServices.tsx";
import {useParams} from "react-router-dom";
import {useSnackbar} from "../../hooks/useSnackbar.ts";
import {TableForm} from "../../components/tables/TableForm.tsx";
import {CircularProgress} from "@mui/material";
import type {ReturnTableDto} from "../../types/tables/ReturnTableDto.ts";
import {useEffect, useState} from "react";
import {TableStatus} from "../../enum/TableStatus.ts";
import type {TableStatusUpdateDto} from "../../types/tables/TableStatusUpdateDto.ts";
import type {TableReservationUpdateDto} from "../../types/tables/TableReservationUpdateDto.ts";

export function TableDetails(){
    const {id} = useParams();
    const [table, setTable] = useState<ReturnTableDto | null>(null);
    const [loading, setLoading] = useState(true);
    const { showSnackbar } = useSnackbar();

    const handleSubmit = async (data: TableFormDto) => {
        if(!table) return;

        const dto: UpdateTableDto = {
            id: id!,
            number: data.number,
            capacity: data.capacity,
        }

        const statusDto: TableStatusUpdateDto = {
            status: data.tableStatus
        }

        const resevationDto: TableReservationUpdateDto = {
            reservationName: data.reservationName,
            reservationTime: data.reservationTime,
        }

        try {
            await updateTable(dto);

            if(data.tableStatus !== TableStatus.Reserved){
                console.log(statusDto);
                await statusUpdateTable(id!, statusDto)
            }

            if(data.tableStatus === TableStatus.Reserved){
                await reservationUpdateTable(id!, resevationDto)
            }

            showSnackbar("Mesa atualizada com sucesso !", "success")

            await loadTable();
        }
        catch (error) {
            showSnackbar("Erro ao atualizar!: " + error, "error");
        }
    }

    const handleCancelReservation = async() => {
        if(!id) return;

        try{
            await cancelReservationTable(id);
            showSnackbar("Reserva cancelada!", "success");
            await loadTable();
        }catch(error){
            showSnackbar("Erro ao cancelar!: " + error, "error");
        }
    }

    const loadTable = async () => {
        if (!id) return;

        try {
            const data = await getTable(id);

            setTable(data);
        }
        finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        loadTable();
    }, [id]);

    if(loading){
        return <CircularProgress/>
    }

    return(
        <>
            <TableForm initialValues={table!} onSubmit={handleSubmit} onCancelReservation={handleCancelReservation}></TableForm>
        </>
    )
}