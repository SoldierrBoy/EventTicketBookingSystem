const BASE_URL = import.meta.env.VITE_API_URL || "http://localhost:5273";

export interface AuthResponse {
  token: string;
  email: string;
  role: string;
}

export async function registerUser(
  email: string,
  password: string,
): Promise<AuthResponse> {
  const res = await fetch(`${BASE_URL}/api/users/register`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ email, password }),
  });

  const data = await res.json();
  if (!res.ok) throw new Error(data.message || "Помилка реєстрації");

  localStorage.setItem("token", data.token);
  return data;
}

export async function loginUser(
  email: string,
  password: string,
): Promise<AuthResponse> {
  const res = await fetch(`${BASE_URL}/api/users/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ email, password }),
  });

  const data = await res.json();
  if (!res.ok) throw new Error(data.message || "Помилка входу");

  localStorage.setItem("token", data.token);
  return data;
}

export function getToken(): string | null {
  return localStorage.getItem("token");
}

export function logout(): void {
  localStorage.removeItem("token");
}
