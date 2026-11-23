import type ReceiveOrderItemDto from "../../types/ordersItens/ReceiveOrderItemDto.ts";
import {useEffect, useState} from "react";
import type {ReturnCategoryDto} from "../../types/categories/ReturnCategoryDto.ts";
import type {ReturnProductDto} from "../../types/products/ReturnProductDto.ts";
import {getAllCategories} from "../../services/CategoryServices.tsx";
import {getAllProducts} from "../../services/ProductServices.tsx";
import {
    Box,
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    FormControl,
    InputLabel,
    MenuItem,
    Select,
    TextField, Typography
} from "@mui/material";

interface Props {
    open: boolean;
    onClose: () => void;
    onSubmit: (data: ReceiveOrderItemDto) => Promise<void>;
}


export function AddOrderItemDialog({open, onClose, onSubmit}: Props){
    const [categories, setCategories] = useState<ReturnCategoryDto[]>([]);
    const [products, setProducts] = useState<ReturnProductDto[]>([]);

    const [categoryId, setCategoryId] = useState<string>("");
    const [productId, setProductId] = useState<string>("");
    const [quantity, setQuantity] = useState(1);
    const [saving, setSaving] = useState(false);

    useEffect(() => {
        if(!open) return;

        const loadData = async () => {
            try{
                const [categoryData, productData] = await Promise.all([
                    getAllCategories(),
                    getAllProducts(),
                ])

                setCategories(categoryData)
                setProducts(productData)
            } catch (error) {
                console.error(error);
            }
        }

        loadData()
    }, [open]);

    const filteredProducts = products.filter(
        product => product.categoryId === categoryId
    );

    const selectedProduct = products.find(
        product => product.id === productId
    );

    const subtotal = (selectedProduct?.price ?? 0) * quantity;

    const handleCategoryChange = (value: string) => {
        setCategoryId(value);
        setProductId("");
    };

    const handleSubmit = async() => {
        if(!productId || quantity < 1) return;

        setSaving(true);

        try{
            await onSubmit({
                itemId: productId,
                quantity
            });

            setCategoryId("");
            setProductId("");
            setQuantity(1);
        } finally {
            setSaving(false);
        }
    }

    const handleClose = () => {
        setCategoryId("");
        setProductId("");
        setQuantity(1);
        onClose();
    };

    return(
        <Dialog
            open={open}
            onClose={handleClose}
            fullWidth
            maxWidth="sm"
        >
            <DialogTitle>Adicionar produto à ordem</DialogTitle>

            <DialogContent
                sx={{
                    display: "flex",
                    flexDirection: "column",
                    gap: 3,
                    pt: 2
                }}
            >
                <FormControl fullWidth>
                    <InputLabel>Categoria</InputLabel>
                    <Select
                        value={categoryId}
                        label="Categoria"
                        onChange={e => handleCategoryChange(e.target.value)}
                    >
                        {categories.map(category => (
                            <MenuItem key={category.id} value={category.id}>
                                {category.name}
                            </MenuItem>
                        ))}
                    </Select>
                </FormControl>
                <FormControl fullWidth disabled={!categoryId}>
                    <InputLabel>Produto</InputLabel>
                    <Select
                        value={productId}
                        label="Produto"
                        onChange={e => setProductId(e.target.value)}
                    >
                        {filteredProducts.map(product => (
                            <MenuItem key={product.id} value={product.id}>
                                {product.name}
                            </MenuItem>
                        ))}
                    </Select>
                </FormControl>

                <TextField
                    label="Quantidade"
                    type="number"
                    fullWidth
                    value={quantity}
                    onChange={e => setQuantity(Math.max(1, Number(e.target.value)))}
                    slotProps={{
                        htmlInput: {min: 1, step: 1}
                }}
                />
                <Box
                    sx={{
                        display: "flex",
                        justifyContent: "space-between",
                    }}
                >
                    <Typography
                        sx={{fontWeight: 'bold'}}
                    >Subtotal</Typography>
                    <Typography
                        sx={{fontWeight: 'bold'}}
                    >
                        {subtotal.toLocaleString("pt-BR", {
                            style: "currency",
                            currency: "BRL"
                        })}
                    </Typography>
                </Box>
            </DialogContent>

            <DialogActions>
                <Button onClick={handleClose}>
                    Cancelar
                </Button>

                <Button
                    variant="contained"
                    disabled={!productId || quantity < 1 || saving}
                    onClick={handleSubmit}
                >
                    {saving ? "Adicionando..." : "Adicionar"}
                </Button>
            </DialogActions>
        </Dialog>
    )
}