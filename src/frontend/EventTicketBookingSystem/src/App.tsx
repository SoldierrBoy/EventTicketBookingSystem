import { Route, BrowserRouter as Router, Routes } from "react-router-dom";
import EventsPage from "./pages/EventsPage";
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<EventsPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route
          path="/events/:id"
          element={<div>Сторінка події (наступний крок)</div>}
        />
      </Routes>
    </Router>
  );
}

export default App;
