import React from "react";
import { BrowserRouter as Router, Routes, Route, Link, Navigate, useNavigate } from "react-router-dom";
import { useAuth } from "./AuthContext";
import Register from "./Register";
import Login from "./Login";
import ScreenTimeSchedules from "./ScreenTimeSchedule";
import "./App.css"; // Add CSS for animations

function Navbar() {
    const { auth, logout } = useAuth();

    return (
        <nav className="navbar">
            <div className="navbar-left">
                <h1>Alnudaar2</h1>
                <ul>
                    {auth.isAuthenticated && (
                        <>
                            <li>
                                <Link to="/schedules">Screen Time Schedules</Link>
                            </li>
                        </>
                    )}
                </ul>
            </div>
            <div className="navbar-right">
                <ul>
                    {auth.isAuthenticated ? (
                        <>
                            <li>
                                <span>{auth.user.username}</span>
                            </li>
                            <li>
                                <button onClick={logout}>Logout</button>
                            </li>
                        </>
                    ) : (
                        <>
                            <li>
                                <Link to="/register">Register</Link>
                            </li>
                            <li>
                                <Link to="/login">Login</Link>
                            </li>
                        </>
                    )}
                </ul>
            </div>
        </nav>
    );
}

function FeatureBox({ name, route }) {
    const navigate = useNavigate();
    return (
        <div className="feature-box" onClick={() => navigate(route)}>
            {name}
        </div>
    );
}

function WelcomePage() {
    return (
        <div className="welcome-page">
            <h1>Welcome to Alnudaar2</h1>
            <p>Please log in to access the features.</p>
            <div>
                <Link to="/login">Login</Link> | <Link to="/register">Register</Link>
            </div>
        </div>
    );
}

function App() {
    const { auth } = useAuth();
    const features = [
        { id: 1, name: "Screen Time Schedules", route: "/schedules" },
        { id: 2, name: "Feature 2 (Future Use)", route: "/feature2" },
        { id: 3, name: "Feature 3 (Future Use)", route: "/feature3" },
    ];

    return (
        <Router>
            <div>
                <Navbar />
                <div className="main-content">
                    <Routes>
                        {/* Public Routes */}
                        <Route path="/register" element={auth.isAuthenticated ? <Navigate to="/" /> : <Register />} />
                        <Route path="/login" element={auth.isAuthenticated ? <Navigate to="/" /> : <Login />} />
                        
                        {/* Protected Routes */}
                        {auth.isAuthenticated ? (
                            <>
                                <Route
                                    path="/"
                                    element={
                                        <div className="features-container">
                                            {features.map((feature) => (
                                                <FeatureBox
                                                    key={feature.id}
                                                    name={feature.name}
                                                    route={feature.route}
                                                />
                                            ))}
                                        </div>
                                    }
                                />
                                <Route path="/schedules" element={<ScreenTimeSchedules />} />
                                {/* Add additional authenticated routes here */}
                            </>
                        ) : (
                            <Route path="*" element={<WelcomePage />} />
                        )}
                    </Routes>
                </div>
            </div>
        </Router>
    );
}

export default App;
