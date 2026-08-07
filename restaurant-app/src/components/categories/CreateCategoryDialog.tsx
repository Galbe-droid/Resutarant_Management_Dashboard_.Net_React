import {useSnackbar} from "../../hooks/useSnackbar.ts";
import {Dialog, DialogContent, DialogTitle} from "@mui/material";
import {CategoryForm} from "./CategoryForm.tsx";
import type {CreateCategoryDto} from "../../types/categories/CreateCategoryDto.ts";
import type {CategoryFormDto} from "../../types/categories/CategoryFormDto.ts";
import {createCategories} from "../../services/CategoryServices.tsx";
import axios from "axios";

interface CreateCategoryDialogProp {
    open: boolean;
    onClose: () => void;
    onCreated: () => void;
}

export function CreateCategoryDialog({open, onClose, onCreated}: CreateCategoryDialogProp) {
    const { showSnackbar } = useSnackbar();

    const handleCreate = async (data:CategoryFormDto) => {
        try{
            const dto: CreateCategoryDto = {
                ...data
            }
            await createCategories(dto);
            onCreated();
            showSnackbar("Sucesso" , "success");
            onClose();
        }catch(error){
            if (axios.isAxiosError(error)) {
                showSnackbar(
                    error.response?.data?.error ?? "Erro ao criar",
                    "error"
                );
            } else {
                showSnackbar("Error: " + error , "error");
            }
        }
    }

    return(
        <Dialog open={open} onClose={onClose}>
            <DialogTitle>Nova Categoria</DialogTitle>

            <DialogContent>
                <CategoryForm onSubmit={handleCreate} />
            </DialogContent>
        </Dialog>
    )
}