import axios from "axios";

export interface ApiResponse<T> {
    success: boolean;
    data: T;
    error: string | null;
}

export const api = axios.create({
    baseURL: import.meta.env.VITE_API_URL,
})

api.interceptors.request.use((config) => {

    const token = localStorage.getItem("accessToken");

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
});