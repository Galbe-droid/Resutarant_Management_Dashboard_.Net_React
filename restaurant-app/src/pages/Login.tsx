import { Box, Button, Card, CardContent, Container, TextField, Typography,} from "@mui/material";
import { useTranslation } from "react-i18next";

export function Login() {
    const { t, i18n } = useTranslation();

    return (
        <Container maxWidth="sm">
            <Box
                sx = {{display: "flex", justifyContent: "center", alignItems: "center", minHeight: "100vh"}}
            >
                <Card sx={{ width: "100%" }}>
                    <Box
                        sx = {{ display: "flex", justifyContent: "flex-end", mb: 2}}
                    >
                        <Button onClick={() => i18n.changeLanguage("pt")}>
                            PT
                        </Button>

                        <Button onClick={() => i18n.changeLanguage("en")}>
                            EN
                        </Button>
                    </Box>
                    <CardContent sx={{ p: 4 }}>
                        <Typography
                            variant="h4"
                            align="center"
                            gutterBottom
                        >
                            {t("login.title")}
                        </Typography>

                        <Typography
                            color="text.secondary"
                            align="center"
                            sx={{ mb: 4 }}
                        >
                            {t("login.subtitle")}
                        </Typography>

                        <Box
                            sx={{ gap: 4, component: "form", display: "flex", flexDirection: "column" }}
                        >
                            <TextField
                                label={t("login.username")}
                            />

                            <TextField
                                label={t("login.password")}
                                type="password"
                            />

                            <Button
                                variant="contained"
                                size="large"
                            >
                                {t("login.button")}
                            </Button>
                        </Box>
                    </CardContent>
                </Card>
            </Box>
        </Container>
    );
}