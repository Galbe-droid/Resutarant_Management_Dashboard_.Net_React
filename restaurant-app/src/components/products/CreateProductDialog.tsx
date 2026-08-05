import {Dialog, DialogContent, DialogTitle} from "@mui/material";
import {ProductForm} from "./ProductForm.tsx";
import {useSnackbar} from "../../hooks/useSnackbar.ts";
import type {CreateProductDto} from "../../types/products/CreateProductDto.ts";
import type {ProductFormDto} from "../../types/products/ProductFormDto.ts";
import {createProduct} from "../../services/ProductServices.tsx";
import type {ReturnCategoryDto} from "../../types/categories/ReturnCategoryDto.ts";

interface CreateTableDialogProp {
    open: boolean;
    onClose: () => void;
    onCreated: () => void;
    categories: ReturnCategoryDto[];
}

export function CreateProductDialog({open, onClose, onCreated, categories}: CreateTableDialogProp) {
    const { showSnackbar } = useSnackbar();

    const handleCreate = async (data: ProductFormDto) => {
        try{
            const dto: CreateProductDto = {
                ...data,
            }
            await createProduct(dto);
            await onCreated();
            showSnackbar("Produto criado com sucesso!", "success");
            onClose();
        }catch(error){
            showSnackbar("Erro inesperado. " + error , "error");
        }
    }

    return(
        <Dialog open={open} onClose={onClose}>
            <DialogTitle>Novo Produto</DialogTitle>

            <DialogContent>
                <ProductForm onSubmit={handleCreate} categories={categories} />
            </DialogContent>
        </Dialog>
    )
}