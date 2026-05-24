import { getToken } from "./auth";

const _RAW_BASE = import.meta.env.VITE_API_URL || "http://localhost:5273";
const BASE_URL = String(_RAW_BASE).replace(/\/+$/, "");

function createAuthHeaders(): HeadersInit {
  const token = getToken();
  return token ? { Authorization: `Bearer ${token}` } : {};
}

export interface EventListItem {
  id: string;
  title: string;
  type: string;
  startsAt: string;
  description: string;
}

export interface EventDetail {
  id: string;
  title: string;
  type: string;
  venue: string;
  startsAt: string;
  createdAt: string;
}

export interface Seat {
  id: string;
  row: string;
  number: number;
  status: "available" | "occupied";
}

export async function getEvents(): Promise<EventListItem[]> {
  const res = await fetch(`${BASE_URL}/api/events`, {
    headers: createAuthHeaders(),
  });
  const data = await res.json();
  if (!res.ok) throw new Error(data.message || "Помилка завантаження подій");

  return data.map((event: any) => ({
    id: event.id,
    title: event.title,
    type: event.type,
    startsAt: event.startsAt,
    description: event.description || event.venue || `Подія типу ${event.type}`,
  }));
}

export async function getEventById(id: string): Promise<EventDetail> {
  const res = await fetch(`${BASE_URL}/api/events/${id}`, {
    headers: createAuthHeaders(),
  });
  const data = await res.json();
  if (!res.ok) throw new Error(data.message || "Подію не знайдено");
  return data;
}

export async function getEventSeats(id: string): Promise<Seat[]> {
  const res = await fetch(`${BASE_URL}/api/events/${id}/seats`, {
    headers: createAuthHeaders(),
  });
  const data = await res.json();
  if (!res.ok) throw new Error(data.message || "Не вдалося завантажити місця");

  return data.map((seat: any) => ({
    id: seat.id,
    row: seat.row,
    number: seat.number,
    status:
      String(seat.status).toLowerCase() === "available"
        ? "available"
        : "occupied",
  }));
}
