import React from 'react';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import './App.css';
import Register from './Register';
import Login from './Login';
import ScreenTimeSchedules from './ScreenTimeSchedule';

function App() {
    return (
        <Router>
            <div>
                <h1>Welcome to Alnudaar2</h1>
                <nav>
                    <ul>
                        <li>
                            <Link to="/register">Register</Link>
                        </li>
                        <li>
                            <Link to="/login">Login</Link>
                        </li>
                        <li>
                            <Link to="/schedules">Screen Time Schedules</Link>
                        </li>
                    </ul>
                </nav>
                <Routes>
                    <Route path="/register" element={<Register />} />
                    <Route path="/login" element={<Login />} />
                    <Route path="/schedules" element={<ScreenTimeSchedules />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;