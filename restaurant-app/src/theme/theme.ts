import { createTheme } from "@mui/material/styles";

export const theme = createTheme({
    palette: {
        mode: "light",
        primary: {
            main: "#2563EB", // Azul
        },
        secondary: {
            main: "#F59E0B", // Âmbar
        },
        background: {
            default: "#F8FAFC",
            paper: "#FFFFFF",
        },
        success: {
            main: "#16A34A",
        },
        warning: {
            main: "#EA580C",
        },
        error: {
            main: "#DC2626",
        },
    },
    shape: {
        borderRadius: 12,
    },
    typography: {
        fontFamily: "'Inter', sans-serif",
        h4: {
            fontWeight: 700,
        },
        h5: {
            fontWeight: 600,
        },
        button: {
            textTransform: "none",
            fontWeight: 600,
        },
    },
    components: {
        MuiCard: {
            styleOverrides: {
                root: {
                    borderRadius: 16,
                    boxShadow:
                        "0 4px 12px rgba(0,0,0,0.05)",
                },
            },
        },
        MuiButton: {
            styleOverrides: {
                root: {
                    borderRadius: 10,
                    padding: "10px 20px",
                },
            },
        },
        MuiTextField: {
            defaultProps: {
                variant: "outlined",
                fullWidth: true,
            },
        },
    },
});