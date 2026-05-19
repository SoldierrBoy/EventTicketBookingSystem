const BASE_URL = import.meta.env.VITE_API_URL || "http://localhost:3000";

export interface EventListItem {
  id: string;
  title: string;
  type: string;
  startsAt: string;
}

export interface EventDetail {
  id: string;
  title: string;
  type: string;
  venue: string;
  startsAt: string;
  createdAt: string;
}

export async function getEvents(): Promise<EventListItem[]> {
  const res = await fetch(`${BASE_URL}/api/events`);
  const data = await res.json();
  if (!res.ok) throw new Error(data.message || "Помилка завантаження подій");
  return data;
}

export async function getEventById(id: string): Promise<EventDetail> {
  const res = await fetch(`${BASE_URL}/api/events/${id}`);
  const data = await res.json();
  if (!res.ok) throw new Error(data.message || "Подію не знайдено");
  return data;
}
