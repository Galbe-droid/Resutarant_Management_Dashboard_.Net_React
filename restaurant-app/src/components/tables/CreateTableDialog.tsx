import {Dialog, DialogContent, DialogTitle} from "@mui/material";
import {TableForm} from "./TableForm.tsx";
import type {TableFormDto} from "../../types/tables/TableFormDto.ts";
import type {CreateTableDto} from "../../types/tables/CreateTableDto.ts";
import {useSnackbar} from "../../hooks/useSnackbar.ts";
import {createTable} from "../../services/TableServices.tsx";
import axios from "axios";
import {useTranslation} from "react-i18next";

interface CreateTableDialogProps {
    open: boolean;
    onClose: () => void;
    onCreated: () => Promise<void>;
}

export function CreateTableDialog({open, onClose, onCreated}: CreateTableDialogProps) {
    const { showSnackbar } = useSnackbar();
    const { t } = useTranslation("table");

    const handleCreate = async (data:TableFormDto) => {
        try{
            const dto: CreateTableDto = {
                ...data
            };
            await createTable(dto);
            await onCreated();
            showSnackbar(t("sucessOnCreate"), "success");
            onClose()
        }catch (error){
            if (axios.isAxiosError(error)) {
                showSnackbar(
                    error.response?.data?.error ?? t("errorOnCreate"),
                    "error"
                );
            } else {
                showSnackbar(t("errorOnCreate"), "error");
            }
        }
    }

    return (
        <Dialog open={open} onClose={onClose}>
            <DialogTitle>Nova Mesa</DialogTitle>

            <DialogContent>
                <TableForm onSubmit={handleCreate}/>
            </DialogContent>
        </Dialog>
    )
}