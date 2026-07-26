import {type ReactNode, useState} from "react";
import {type AlertColor} from "@mui/material";
import {AppSnackbar} from "../components/common/AppSnackbar.tsx";
import { SnackbarContext } from "../contexts/SnackbarContext.tsx";

export function SnackbarProvider({children}: {children: ReactNode}) {
    const [open, setOpen] = useState(false);
    const [message, setMessage] = useState("");
    const [severity, setSeverity] = useState<AlertColor>("success");

    const showSnackbar = (message: string, severity: AlertColor) => {
        setSeverity(severity);
        setMessage(message);
        setOpen(true);
    }


    return(
        <SnackbarContext.Provider value={{showSnackbar}}>
            {children}
            <AppSnackbar
                open={open}
                severity={severity}
                message={message}
                onClose={() => setOpen(false)}
            />
        </SnackbarContext.Provider>
    )
}