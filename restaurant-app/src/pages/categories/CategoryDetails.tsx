import {useNavigate, useParams} from "react-router-dom";
import {CategoryForm} from "../../components/categories/CategoryForm.tsx";
import {CircularProgress} from "@mui/material";
import {useEffect, useState} from "react";
import type {ReturnCategoryDto} from "../../types/categories/ReturnCategoryDto.ts";
import {deleteCategories, getCategories, updateCategories} from "../../services/CategoryServices.tsx";
import type {CategoryFormDto} from "../../types/categories/CategoryFormDto.ts";
import {useSnackbar} from "../../hooks/useSnackbar.ts";
import type {UpdateCategoryDto} from "../../types/categories/UpdateCategoryDto.ts";

export function CategoryDetails() {
    const { id } = useParams();
    const { showSnackbar } = useSnackbar();
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);
    const [category, setCategory] = useState<ReturnCategoryDto | null>();

    const loadCategory = async() => {
        if(!id) return;

        try{
            const data = await getCategories(id);
            setCategory(data);
        }finally {
            setLoading(false);
        }
    }

    const handleUpdate = async(data: CategoryFormDto) => {
        if(!id) return;

        try{
            if(id === '11111111-1111-1111-1111-111111111111'){
                showSnackbar( "Cannot update base", "error");
            }
            else{
                const dto: UpdateCategoryDto = {
                    ...data,
                }
                await updateCategories(id!, dto);
                showSnackbar( "Categoria Atualizada", "success");
            }
        }catch (error){
            showSnackbar( "Error: " + error, "error");
        }
    }

    const handleDelete = async() => {
        if(!id) return;
        try{
            if(id === '11111111-1111-1111-1111-111111111111'){
                showSnackbar( "Cannot delete base", "error");
            }
            else{
                await deleteCategories(id!);
                showSnackbar( "Categoria Deletada", "success");
                navigate("/categories")
            }

        }catch(error){
            showSnackbar( "Error: " + error, "error");
        }
    }

    useEffect(() => {
        const load = async () => {
            await Promise.all([
                loadCategory()
            ]);
        };

        load();
    }, []);

    if(loading){
        return <CircularProgress/>
    }

    return(
        <>
            <CategoryForm initialValues={category!} onSubmit={handleUpdate} onDeleteProduct={handleDelete}/>
        </>
    )
}