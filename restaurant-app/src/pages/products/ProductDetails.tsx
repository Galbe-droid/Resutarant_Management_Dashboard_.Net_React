import {useNavigate, useParams} from "react-router-dom";
import type {ReturnProductDto} from "../../types/products/ReturnProductDto.ts";
import {useEffect, useState} from "react";
import {ProductForm} from "../../components/products/ProductForm.tsx";
import type {ReturnCategoryDto} from "../../types/categories/ReturnCategoryDto.ts";
import {deleteProduct, getProduct, updateProduct} from "../../services/ProductServices.tsx";
import {useSnackbar} from "../../hooks/useSnackbar.ts";
import type {ProductFormDto} from "../../types/products/ProductFormDto.ts";
import type {UpdateProductDto} from "../../types/products/UpdateProduct.ts";
import {CircularProgress} from "@mui/material";
import {getAllCategories} from "../../services/CategoryServices.tsx";
import {useTranslation} from "react-i18next";

export function ProductDetails() {
    const {id} = useParams();
    const {t} = useTranslation("product");
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);
    const [categories, setCategories] = useState<ReturnCategoryDto[]>();
    const [product, setProduct] = useState<ReturnProductDto | null>(null)
    const { showSnackbar } = useSnackbar();

    const loadCategories = async () => {
        try {
            const response = await getAllCategories();
            setCategories(response);
        } catch (error) {
            console.error(error);
        }
    };

    const handleSubmit = async (data: ProductFormDto) => {
        if(!id) return;

        const dto: UpdateProductDto = {
            ...data,
        }

        try{
            await updateProduct(id, dto);
            showSnackbar( t("successUpdate"), "success")
        } catch(error){
            showSnackbar( t("errorUpdate") + error, "error");
        }
    }

    const handleDelete = async () => {
        if(!id) return;

        try{
            await deleteProduct(id);
            showSnackbar( t("successDelete"), "success")
            navigate("/products");
        } catch(error){
            showSnackbar( t("errorDelete") + error, "error");
        }
    }

    const loadProduct = async () => {
        if(!id) return;

        try{
            const data = await getProduct(id);
            setProduct(data);

        }finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        const load = async () => {
            await Promise.all([
                loadProduct(),
                loadCategories()
            ]);
        };

        load();
    }, []);

    if(loading){
        return <CircularProgress/>
    }

    return(
        <>
            <ProductForm initialValues={product!} onSubmit={handleSubmit} categories={categories!} onDeleteProduct={handleDelete} />
        </>
    )
}