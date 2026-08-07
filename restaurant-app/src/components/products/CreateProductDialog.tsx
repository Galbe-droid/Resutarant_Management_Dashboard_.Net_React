import {Dialog, DialogContent, DialogTitle} from "@mui/material";
import {ProductForm} from "./ProductForm.tsx";
import {useSnackbar} from "../../hooks/useSnackbar.ts";
import type {CreateProductDto} from "../../types/products/CreateProductDto.ts";
import type {ProductFormDto} from "../../types/products/ProductFormDto.ts";
import {createProduct} from "../../services/ProductServices.tsx";
import type {ReturnCategoryDto} from "../../types/categories/ReturnCategoryDto.ts";
import {useTranslation} from "react-i18next";
import axios from "axios";

interface CreateProductDialogProp {
    open: boolean;
    onClose: () => void;
    onCreated: () => void;
    categories: ReturnCategoryDto[];
}

export function CreateProductDialog({open, onClose, onCreated, categories}: CreateProductDialogProp) {
    const { showSnackbar } = useSnackbar();
    const { t } = useTranslation("product");

    const handleCreate = async (data: ProductFormDto) => {
        try{
            const dto: CreateProductDto = {
                ...data,
            }
            await createProduct(dto);
            await onCreated();
            showSnackbar(t("successCreation"), "success");
            onClose();
        }catch(error){
            if (axios.isAxiosError(error)) {
                showSnackbar(
                    error.response?.data?.error ?? t("errorCreation"),
                    "error"
                );
            } else {
                showSnackbar(t("errorCreation") + error , "error");
            }
        }
    }

    return(
        <Dialog open={open} onClose={onClose}>
            <DialogTitle>{t("newButtonDashboard")}</DialogTitle>

            <DialogContent>
                <ProductForm onSubmit={handleCreate} categories={categories} />
            </DialogContent>
        </Dialog>
    )
}