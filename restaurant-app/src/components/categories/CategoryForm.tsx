import type {ReturnCategoryDto} from "../../types/categories/ReturnCategoryDto.ts";
import {useForm} from "react-hook-form";
import type {CategoryFormDto} from "../../types/categories/CategoryFormDto.ts";
import {useEffect} from "react";
import {Box, Button, TextField} from "@mui/material";

interface CategoryFormProps{
    initialValues?: ReturnCategoryDto;
    id?: string;
    onSubmit: (data: CategoryFormDto) => void;
    onDeleteProduct?: () => void;
}

export function CategoryForm({initialValues, onSubmit, onDeleteProduct}: CategoryFormProps) {
    const {register, handleSubmit, reset, formState: { errors }} = useForm<CategoryFormDto>({
        defaultValues:{
            name: initialValues?.name ?? "",
        }
    });

    useEffect(() =>{
        if(!initialValues) return;

        reset({
            name: initialValues?.name,
        })
    }, [initialValues, reset])

    return(
        <Box component={"form"} onSubmit={handleSubmit(onSubmit)}
             sx={{
                 display: "flex",
                 flexDirection: "column",
                 gap:3,
                 padding: 2
             }}
        >
            <TextField fullWidth label={"Nome"} {...register("name")}
                       error={!!errors.name} helperText={errors.name?.message}/>

            <Button type={"submit"}>{initialValues ? "Editar Categoria" : "Nova Categoria"}</Button>
            {initialValues && (
                <>
                    <Button color="error" variant="outlined" onClick={onDeleteProduct}>Deletar Categoria</Button>
                </>
            )}
        </Box>
    )
}