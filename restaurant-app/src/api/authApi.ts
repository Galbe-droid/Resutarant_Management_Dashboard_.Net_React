import { api } from "./axios";
import type { LoginRequest } from "../types/LoginRequest.ts";

export async function loginAuth(data: LoginRequest){
    const response = await api.post("/Auth/login", data);
    return response.data;
}