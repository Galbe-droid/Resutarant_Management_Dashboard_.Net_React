import type {AlertColor} from "@mui/material";
import {createContext} from "react";

interface SnackbarContextType {
    showSnackbar: (
        message: string,
        severity: AlertColor,
    ) => void;
}

export const SnackbarContext = createContext<SnackbarContextType | null>(null);