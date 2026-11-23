
import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    TextField
} from "@mui/material";
import { useEffect, useState } from "react";
import type ReturnOrderItemDto from "../../types/ordersItens/ReturnOrderItemDto";

interface UpdateOrderItemDialogProps {
    open: boolean;
    item: ReturnOrderItemDto | null;
    onClose: () => void;
    onSubmit: (itemId: string, quantity: number) => Promise<void>;
}

export function UpdateOrderItemDialog({
                                          open,
                                          item,
                                          onClose,
                                          onSubmit
                                      }: UpdateOrderItemDialogProps) {

    const [quantity, setQuantity] = useState(1);
    const [saving, setSaving] = useState(false);

    useEffect(() => {
        if (item) {
            setQuantity(item.quantity);
        }
    }, [item]);

    const handleSubmit = async () => {
        if (!item || quantity < 1) return;

        try {
            setSaving(true);
            await onSubmit(item.id, quantity);
            onClose();
        } finally {
            setSaving(false);
        }
    };

    return (
        <Dialog open={open} onClose={onClose} fullWidth maxWidth="xs">
            <DialogTitle>Update Order Item</DialogTitle>

            <DialogContent>
                <TextField
                    fullWidth
                    margin="normal"
                    label="Product"
                    value={item?.productName ?? ""}
                    disabled
                />

                <TextField
                    fullWidth
                    margin="normal"
                    type="number"
                    label="Quantity"
                    value={quantity}
                    onChange={(e) =>
                        setQuantity(Math.max(1, Number(e.target.value)))
                    }
                    slotProps={{
                        htmlInput: {min: 1, step: 1}
                    }}
                />
            </DialogContent>

            <DialogActions>
                <Button onClick={onClose} disabled={saving}>
                    Cancel
                </Button>

                <Button
                    variant="contained"
                    onClick={handleSubmit}
                    disabled={saving || !item || quantity < 1}
                >
                    {saving ? "Saving..." : "Update"}
                </Button>
            </DialogActions>
        </Dialog>
    );
}
