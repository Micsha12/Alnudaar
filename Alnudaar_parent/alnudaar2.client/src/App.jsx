import React, { useState, useEffect } from "react";
import { BrowserRouter as Router, Routes, Route, Link, Navigate, useNavigate } from "react-router-dom";
import { useAuth } from "./AuthContext";
import Register from "./Register";
import Login from "./Login";
import AppUsageReport from "./AppUsageReport";
import ScreenTimeSchedules from "./ScreenTimeSchedule";
import RegisterDevice from "./RegisterDevice";
import ManageDevices from "./ManageDevices";
import BlockRules from "./BlockRules";
import "./App.css";

function Navbar() {
    const { auth, logout } = useAuth();
    const [dropdownOpen, setDropdownOpen] = useState(false);

    return (
        <nav className="navbar">
            <div className="navbar-left">
                <h1>
                    <Link to="/" className="navbar-brand">Alnudaar</Link>
                </h1>
                <ul>
                    {auth.isAuthenticated && (
                        <>
                            <li>
                                <Link to="/schedules">Screen Time Schedules</Link>
                            </li>
                            <li>
                                <Link to="/block-rules">Website & App Blocking</Link> {/* Add link to BlockRules */}
                            </li>
                            <li>
                                <Link to="/register-device">Register Device</Link>
                            </li>
                        </>
                    )}
                </ul>
            </div>
            <div className="navbar-right">
                <ul>
                    {auth.isAuthenticated ? (
                        <>
                            <li className="dropdown">
                                <span onClick={() => setDropdownOpen(!dropdownOpen)}>{auth.user.username}</span>
                                {dropdownOpen && (
                                    <ul className="dropdown-menu">
                                        <li>
                                            <Link to="/manage-devices">Manage Devices</Link>
                                        </li>
                                        <li>
                                            <button onClick={logout}>Logout</button>
                                        </li>
                                    </ul>
                                )}
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
    const { selectedDevice, setSelectedDevice, auth } = useAuth();
    const [devices, setDevices] = useState([]);

    useEffect(() => {
        // Fetch devices for the logged-in user
        const fetchDevices = async () => {
            if (!auth.user) return;
            const response = await fetch(`https://localhost:7200/api/devices/user/${auth.user.userID}`);
            if (response.ok) {
                const data = await response.json();
                setDevices(data);
                if (data.length === 1) {
                    setSelectedDevice(data[0]); // Auto-select if only one device
                }
            } else {
                console.error('Failed to fetch devices');
            }
        };

        fetchDevices();
    }, [auth.user, setSelectedDevice]);

    const handleDeviceChange = (e) => {
        const deviceID = Number(e.target.value);
        const selected = devices.find((device) => device.deviceID === deviceID);
        setSelectedDevice(selected);
    };

    const handleNavigate = () => {
        if (!selectedDevice) {
            alert('Please select a device before proceeding.');
            return;
        }
        navigate(route);
    };

    return (
        <div className="feature-box">
            <h3>{name}</h3>
            <div>
                <label htmlFor="device-select">Select Device:</label>
                <select
                    id="device-select"
                    value={selectedDevice?.deviceID || ''}
                    onChange={handleDeviceChange}
                >
                    <option value="" disabled>
                        {devices.length === 0 ? 'No devices available' : 'Select a device'}
                    </option>
                    {devices.map((device) => (
                        <option key={device.deviceID} value={device.deviceID}>
                            {device.name}
                        </option>
                    ))}
                </select>
            </div>
            <button onClick={handleNavigate}>Go to {name}</button>
        </div>
    );
}

function WelcomePage() {
    return (
        <div className="welcome-page">
            <h1>Welcome to Alnudaar</h1>
            <p>Please log in to access the features.</p>
        </div>
    );
}

function App() {
    const { auth } = useAuth();
    const features = [
        { id: 1, name: "Screen Time Schedules", route: "/schedules" },
        { id: 2, name: "Website & App Blocking", route: "/block-rules" },
        { id: 3, name: "App Usage Report", route: "/appusagereport" },
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
                                <Route path="/block-rules" element={<BlockRules />} />
                                <Route path="/appusagereport" element={<AppUsageReport />} />
                                <Route path="/register-device" element={<RegisterDevice />} />
                                <Route path="/manage-devices" element={<ManageDevices />} />
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