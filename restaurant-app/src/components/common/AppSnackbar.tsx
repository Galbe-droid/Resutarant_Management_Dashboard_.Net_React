import {Alert, type AlertColor, Snackbar} from "@mui/material";

interface AppSnackbarProps {
    open: boolean;
    severity: AlertColor;
    message: string;
    onClose: () => void;
}


export function AppSnackbar({open, severity, message, onClose}: AppSnackbarProps) {
    return (
        <Snackbar
            open={open}
            autoHideDuration={6000}
            onClose={onClose}
            anchorOrigin={{
                vertical: "bottom",
                horizontal: "right"
            }}
        >
            <Alert
                severity={severity}
                onClose={onClose}
                variant={"filled"}
            >
                {message}
            </Alert>
        </Snackbar>
    )
}