import {TableForm} from "../../components/tables/TableForm.tsx";
import {useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import type {ReturnTableDto} from "../../types/tables/ReturnTableDto.ts";
import {getTable} from "../../services/TableServices.tsx";
import type {UpdateTableDto} from "../../types/tables/UpdateTableDto.ts";
import type {TableFormDto} from "../../types/tables/TableFormDto.ts";
import {CircularProgress, Typography} from "@mui/material";
import {useSnackbar} from "../../hooks/useSnackbar.ts";

export function TablePage() {
    const id = useParams();
    const [table, setTable] = useState<ReturnTableDto | null>(null)
    const [loading, setLoading] = useState<boolean>(true)
    const { showSnackbar } = useSnackbar();

    useEffect(() => {
        try{
            if(!id) return;
            async function loadTable() {
                setLoading(true);
                const data = await getTable(id);
                setTable(data)
            }
            loadTable();
        }
        finally {
            setLoading(false);
        }
    }, [id])

    if(!table){
        return <Typography>Mesa Não Encontrada...</Typography>
    }

    if(loading) {
        return <CircularProgress/>
    }

    const handleUpdate = async (data: TableFormDto) => {
        try{
            console.log(data);
            const dto: UpdateTableDto = {
                id: table.id,
                ...data
            }
            console.log(dto);
            showSnackbar("Mesa atualizada com sucesso !", "success")
        } catch (error){
            console.error(error)
            showSnackbar("Erro ao atualizar!", "error")
        }
    }

    return (
        <>
            <TableForm initialValues={table!} onSubmit={handleUpdate}/>
        </>
    )
}