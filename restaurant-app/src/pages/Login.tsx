import { Box, Button, Card, CardContent, Container, TextField, Typography,} from "@mui/material";
import { useTranslation } from "react-i18next";
import {useNavigate} from "react-router-dom";
import {useAuth} from "../hooks/useAuth.ts";
import type {LoginRequest} from "../types/LoginRequest.ts";
import {useForm} from "react-hook-form";

export function Login() {
    const { t, i18n } = useTranslation();
        const { register, handleSubmit, formState: { errors } } = useForm<LoginRequest>();

    const auth = useAuth();

    const navigate = useNavigate();

    const onSubmit = async (data: LoginRequest) => {

        const success = await auth.login(data);

        if (success) {
            navigate("/dashboard");
        }
    };

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
                            component="form"
                            sx={{ gap: 4, display: "flex", flexDirection: "column" }}
                            onSubmit={handleSubmit(onSubmit)}
                        >
                            <TextField
                                label={t("login.username")}
                                {...register("login", {required: "Required"})}
                                error={!!errors.login}
                                helperText={errors.login?.message}
                            />

                            <TextField
                                label={t("login.password")}
                                type="password"
                                {...register("password", {required: "Required"})}
                                error={!!errors.password}
                                helperText={errors.password?.message}
                            />

                            <Button
                                variant="contained"
                                size="large"
                                type="submit"
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