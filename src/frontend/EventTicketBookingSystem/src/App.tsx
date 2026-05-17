import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";

function App() {
  const path = window.location.pathname;

  if (path === "/register") return <RegisterPage />;
  if (path === "/login") return <LoginPage />;

  return (
    <div style={styles.wrapper}>
      <div style={styles.card}>
        <h1 style={styles.title}>EventTicketSystem</h1>
        <p style={styles.subtitle}>Купуй квитки на найкращі події</p>
        <div style={styles.buttons}>
          <a href="/login" style={styles.btnPrimary}>
            Увійти
          </a>
          <a href="/register" style={styles.btnSecondary}>
            Зареєструватись
          </a>
        </div>
      </div>
    </div>
  );
}

const styles: Record<string, React.CSSProperties> = {
  wrapper: {
    minHeight: "100vh",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    backgroundColor: "#f4f4f4",
  },
  card: {
    backgroundColor: "#fff",
    padding: "3rem 2rem",
    borderRadius: "12px",
    boxShadow: "0 4px 20px rgba(0,0,0,0.1)",
    textAlign: "center",
    maxWidth: "400px",
    width: "100%",
  },
  title: {
    fontSize: "2rem",
    marginBottom: "0.5rem",
  },
  subtitle: {
    color: "#666",
    marginBottom: "2rem",
  },
  buttons: {
    display: "flex",
    gap: "1rem",
    justifyContent: "center",
  },
  btnPrimary: {
    padding: "0.75rem 1.5rem",
    borderRadius: "8px",
    backgroundColor: "#4f46e5",
    color: "#fff",
    textDecoration: "none",
    fontSize: "1rem",
  },
  btnSecondary: {
    padding: "0.75rem 1.5rem",
    borderRadius: "8px",
    backgroundColor: "#fff",
    color: "#4f46e5",
    border: "1px solid #4f46e5",
    textDecoration: "none",
    fontSize: "1rem",
  },
};

export default App;
