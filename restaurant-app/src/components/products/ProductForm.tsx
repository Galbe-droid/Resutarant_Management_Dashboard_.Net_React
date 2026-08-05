
import type {ReturnProductDto} from "../../types/products/ReturnProductDto.ts";
import type {ProductFormDto} from "../../types/products/ProductFormDto.ts";
import {Controller, useForm} from "react-hook-form";
import {useEffect} from "react";
import {Box, Button, MenuItem, TextField} from "@mui/material";
import type {ReturnCategoryDto} from "../../types/categories/ReturnCategoryDto.ts";

interface ProductFormProps {
    initialValues?: ReturnProductDto
    onSubmit: (data: ProductFormDto) => void
    onDeleteProduct?: () => void;
    categories: ReturnCategoryDto[];
}
export function ProductForm({initialValues, onSubmit, onDeleteProduct, categories}: ProductFormProps) {
    const {register, handleSubmit, control, reset, formState: { errors }} = useForm<ProductFormDto>({
        defaultValues:{
            name: initialValues?.name ?? "",
            price: initialValues?.price ?? 0.00,
            description: initialValues?.description ?? "",
            imageURL: initialValues?.imageURL ?? "",
            categoryId: initialValues?.categoryId ?? "",
        }
    });

    useEffect(() =>{
        if(!initialValues) return;

        reset({
            name: initialValues?.name,
            price: initialValues?.price,
            description: initialValues?.description,
            imageURL: initialValues?.imageURL,
            categoryId: initialValues?.categoryId,
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

            <TextField fullWidth label={"Price"} type={"number"} slotProps={{htmlInput: { step: "0.01", min: "0" }}} {...register("price",{valueAsNumber: true})}
                       error={!!errors.price} helperText={errors.price?.message}/>

            <TextField fullWidth label={"Description"} type={"text"} {...register("description")}
                       error={!!errors.description} helperText={errors.description?.message}/>

            <Controller
                name="categoryId"
                control={control}
                render={({ field }) => (
                    <TextField
                        {...field}
                        select
                        fullWidth
                        label="Categoria"
                        error={!!errors.categoryId}
                        helperText={errors.categoryId?.message}
                    >
                        {categories?.map(category => (
                            <MenuItem
                                key={category.id}
                                value={category.id}
                            >
                                {category.name}
                            </MenuItem>
                        ))}
                    </TextField>
                )}
            />

            <Button type={"submit"}>{initialValues ? "Editar Produto" : "Novo Produto"}</Button>
            {initialValues && (
                <>
                    <Button color="error" variant="outlined" onClick={onDeleteProduct}>Deletar Produto</Button>
                </>
            )}
        </Box>
    )
}