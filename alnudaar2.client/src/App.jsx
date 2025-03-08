import React from 'react';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import './App.css';
import ScreenTimeSchedules from './ScreenTimeSchedule';

function App() {
    return (
        <Router>
            <div>
                <h1>Welcome to Alnudaar2</h1>
                <nav>
                    <ul>
                        <li>
                            <Link to="/schedules">Screen Time Schedules</Link>
                        </li>
                    </ul>
                </nav>
                <Routes>
                    <Route path="/schedules" element={<ScreenTimeSchedules />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;