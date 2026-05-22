import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { type EventListItem, getEvents } from "../api/events";

export default function EventsPage() {
  const [events, setEvents] = useState<EventListItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    getEvents()
      .then(setEvents)
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <div style={styles.center}>Завантаження...</div>;
  if (error) return <div style={styles.center}>Помилка: {error}</div>;

  return (
    <div style={styles.page}>
      <header style={styles.header}>
        <h1 style={styles.logo}>EventTicketSystem</h1>
        <nav style={styles.nav}>
          <Link to="/login" style={styles.navLink}>
            Увійти
          </Link>
          <Link to="/register" style={styles.navLink}>
            Реєстрація
          </Link>
        </nav>
      </header>

      <main style={styles.main}>
        <h2 style={styles.title}>Каталог подій</h2>
        {events.length === 0 ? (
          <p style={styles.empty}>Подій поки немає</p>
        ) : (
          <div style={styles.grid}>
            {events.map((event) => (
              <div key={event.id} style={styles.card}>
                <div style={styles.cardBody}>
                  <span style={styles.badge}>{event.type}</span>
                  <h3 style={styles.eventTitle}>{event.title}</h3>
                  <p style={styles.date}>
                    📅{" "}
                    {new Date(event.startsAt).toLocaleDateString("uk-UA", {
                      day: "numeric",
                      month: "long",
                      year: "numeric",
                      hour: "2-digit",
                      minute: "2-digit",
                    })}
                  </p>
                  <p style={styles.description}>{event.description}</p>
                </div>
                <div style={styles.cardFooter}>
                  <Link to={`/events/${event.id}`} style={styles.btn}>
                    Купити квиток →
                  </Link>
                </div>
              </div>
            ))}
          </div>
        )}
      </main>
    </div>
  );
}

const styles: Record<string, React.CSSProperties> = {
  page: { minHeight: "100vh", backgroundColor: "#f4f4f4" },
  header: {
    backgroundColor: "#fff",
    padding: "1rem 2rem",
    display: "flex",
    alignItems: "center",
    justifyContent: "space-between",
    boxShadow: "0 1px 4px rgba(0,0,0,0.08)",
  },
  logo: { fontSize: "1.25rem", fontWeight: 600, margin: 0, color: "#4f46e5" },
  nav: { display: "flex", gap: "1rem" },
  navLink: { color: "#666", textDecoration: "none", fontSize: "0.9rem" },
  main: { maxWidth: "1100px", margin: "0 auto", padding: "2rem 1rem" },
  title: { fontSize: "1.75rem", marginBottom: "1.5rem", color: "#1a1a1a" },
  empty: { color: "#666", textAlign: "center", marginTop: "4rem" },
  grid: {
    display: "grid",
    gridTemplateColumns: "repeat(auto-fill, minmax(300px, 1fr))",
    gap: "1.5rem",
  },
  card: {
    backgroundColor: "#fff",
    borderRadius: "12px",
    boxShadow: "0 2px 12px rgba(0,0,0,0.08)",
    display: "flex",
    flexDirection: "column",
    justifyContent: "space-between",
    overflow: "hidden",
  },
  cardBody: { padding: "1.5rem" },
  cardFooter: {
    padding: "1rem 1.5rem",
    borderTop: "1px solid #f0f0f0",
    display: "flex",
    justifyContent: "flex-end",
  },
  badge: {
    display: "inline-block",
    padding: "0.25rem 0.75rem",
    borderRadius: "999px",
    backgroundColor: "#ede9fe",
    color: "#4f46e5",
    fontSize: "0.75rem",
    fontWeight: 600,
    marginBottom: "0.75rem",
  },
  eventTitle: {
    fontSize: "1.1rem",
    fontWeight: 600,
    marginBottom: "0.5rem",
    color: "#1a1a1a",
  },
  date: { fontSize: "0.875rem", color: "#666", margin: 0 },
  description: { fontSize: "0.95rem", color: "#4b5563", marginTop: "0.85rem", lineHeight: 1.5 },
  btn: {
    padding: "0.5rem 1.25rem",
    borderRadius: "8px",
    backgroundColor: "#4f46e5",
    color: "#fff",
    textDecoration: "none",
    fontSize: "0.875rem",
  },
  center: {
    minHeight: "100vh",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    color: "#666",
  },
};
