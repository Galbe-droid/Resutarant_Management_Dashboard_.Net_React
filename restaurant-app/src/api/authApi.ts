import { api } from "./axios";
import type { LoginRequest } from "../types/user/LoginRequest.ts";

export async function loginAuth(data: LoginRequest){
    const response = await api.post("/Auth/login", data);
    return response.data;
}