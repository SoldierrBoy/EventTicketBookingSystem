import { useEffect, useMemo, useState } from "react";
import { useParams } from "react-router-dom";
import { getEventById, getEventSeats, type Seat } from "../api/events";

export default function EventDetailPage() {
  const { id } = useParams<{ id: string }>();
  const [event, setEvent] = useState<any>(null);
  const [seats, setSeats] = useState<Seat[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [selected, setSelected] = useState<Record<string, boolean>>({});

  useEffect(() => {
    if (!id) return;
    setLoading(true);
    Promise.all([getEventById(id), getEventSeats(id)])
      .then(([evt, seats]) => {
        setEvent(evt);
        setSeats(seats);
      })
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, [id]);

  const seatsByRow = useMemo(() => {
    const map = new Map<string, Seat[]>();
    seats.forEach((s) => {
      const arr = map.get(s.row) || [];
      arr.push(s);
      map.set(s.row, arr);
    });
    for (const [, arr] of map) arr.sort((a, b) => a.number - b.number);
    return Array.from(map.entries()).sort((a, b) => (a[0] > b[0] ? 1 : -1));
  }, [seats]);

  function toggleSelect(s: Seat) {
    if (s.status === "occupied") return;
    setSelected((prev) => {
      const copy = { ...prev };
      if (copy[s.id]) delete copy[s.id];
      else copy[s.id] = true;
      return copy;
    });
  }

  function placeOrder() {
    const ids = Object.keys(selected);
    console.log("Selected seats:", ids);
    // For now show in UI briefly
    alert(`Обрані місця: ${ids.join(", ") || "(немає)"}`);
  }

  if (loading) return <div style={styles.center}>Завантаження...</div>;
  if (error) return <div style={styles.center}>Помилка: {error}</div>;
  if (!event) return <div style={styles.center}>Подію не знайдено</div>;

  return (
    <div style={styles.page}>
      <main style={styles.main}>
        <h2 style={styles.title}>{event.title}</h2>
        <p style={styles.meta}>
          📅 {new Date(event.startsAt).toLocaleString("uk-UA")}
        </p>

        <section style={styles.hall}>
          <h3 style={styles.sectionTitle}>Схема залу</h3>
          <div style={styles.screen}>Екран</div>
          <div style={styles.rows}>
            {seatsByRow.map(([rowLabel, rowSeats]) => (
              <div key={rowLabel} style={styles.row}>
                <div style={styles.rowLabel}>{rowLabel}</div>
                <div style={styles.seatRow}>
                  {rowSeats.map((s) => {
                    const isSelected = !!selected[s.id];
                    const occupied = s.status === "occupied";
                    return (
                      <button
                        key={s.id}
                        onClick={() => toggleSelect(s)}
                        style={{
                          ...styles.seat,
                          ...(occupied
                            ? styles.seatOccupied
                            : styles.seatAvailable),
                          ...(isSelected ? styles.seatSelected : {}),
                        }}
                        disabled={occupied}
                        title={`Ряд ${s.row} місце ${s.number}`}
                      >
                        {s.number}
                      </button>
                    );
                  })}
                </div>
              </div>
            ))}
          </div>
        </section>

        <div style={styles.actionBar}>
          <button onClick={placeOrder} style={styles.orderBtn}>
            Оформити замовлення
          </button>
        </div>
      </main>
    </div>
  );
}

const styles: Record<string, React.CSSProperties> = {
  page: { minHeight: "100vh", backgroundColor: "#f4f4f4" },
  main: { maxWidth: "900px", margin: "0 auto", padding: "2rem 1rem" },
  title: { fontSize: "1.5rem", marginBottom: "0.25rem" },
  meta: { color: "#666", marginTop: 0, marginBottom: "1.25rem" },
  hall: {
    backgroundColor: "#fff",
    padding: "1rem",
    borderRadius: 10,
    boxShadow: "0 1px 6px rgba(0,0,0,0.06)",
  },
  sectionTitle: { marginTop: 0, marginBottom: "0.5rem" },
  screen: {
    textAlign: "center",
    padding: "0.5rem",
    background: "#e6f0ff",
    borderRadius: 6,
    marginBottom: "1rem",
    color: "#1e3a8a",
  },
  rows: { display: "flex", flexDirection: "column", gap: "0.75rem" },
  row: { display: "flex", alignItems: "center", gap: "0.75rem" },
  rowLabel: { width: 40, textAlign: "right", color: "#333", fontWeight: 600 },
  seatRow: { display: "flex", gap: "0.5rem", flexWrap: "wrap" },
  seat: {
    width: 40,
    height: 40,
    borderRadius: 6,
    border: "none",
    cursor: "pointer",
    color: "#fff",
    fontWeight: 600,
  },
  seatOccupied: { backgroundColor: "#9ca3af", cursor: "not-allowed" },
  seatAvailable: { backgroundColor: "#2563eb" },
  seatSelected: { outline: "3px solid #fde68a", transform: "scale(1.05)" },
  actionBar: { marginTop: "1rem", display: "flex", justifyContent: "flex-end" },
  orderBtn: {
    padding: "0.6rem 1.2rem",
    backgroundColor: "#10b981",
    color: "#fff",
    border: "none",
    borderRadius: 8,
    cursor: "pointer",
  },
  center: {
    minHeight: "60vh",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    color: "#666",
  },
};
