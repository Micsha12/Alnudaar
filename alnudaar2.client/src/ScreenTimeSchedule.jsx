import React, { useEffect, useState } from 'react';

function ScreenTimeSchedules() {
    const [schedules, setSchedules] = useState([]);
    const [loading, setLoading] = useState(true);
    const [newSchedule, setNewSchedule] = useState({
        userID: '',
        deviceID: '',
        startTime: '',
        endTime: '',
        dayOfWeek: ''
    });

    useEffect(() => {
        fetchScreenTimeSchedules();
    }, []);

    const fetchScreenTimeSchedules = async () => {
        const response = await fetch('https://localhost:7200/api/screentimeschedule');
        const data = await response.json();
        setSchedules(data);
        setLoading(false);
    };

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setNewSchedule({ ...newSchedule, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const response = await fetch('https://localhost:7200/api/screentimeschedule', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newSchedule)
        });
        if (response.ok) {
            fetchScreenTimeSchedules();
            setNewSchedule({
                userID: '',
                deviceID: '',
                startTime: '',
                endTime: '',
                dayOfWeek: ''
            });
        }
    };

    const renderSchedulesTable = (schedules) => {
        return (
            <table className="table table-striped" aria-labelledby="tableLabel">
                <thead>
                    <tr>
                        <th>Schedule ID</th>
                        <th>User ID</th>
                        <th>Device ID</th>
                        <th>Start Time</th>
                        <th>End Time</th>
                        <th>Day of Week</th>
                    </tr>
                </thead>
                <tbody>
                    {schedules.map(schedule =>
                        <tr key={schedule.scheduleID}>
                            <td>{schedule.scheduleID}</td>
                            <td>{schedule.userID}</td>
                            <td>{schedule.deviceID}</td>
                            <td>{schedule.startTime}</td>
                            <td>{schedule.endTime}</td>
                            <td>{schedule.dayOfWeek}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    };

    return (
        <div>
            <h1 id="tableLabel">Screen Time Schedules</h1>
            <p>This component demonstrates fetching data from the server and adding new schedules.</p>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>User ID:</label>
                    <input type="text" name="userID" value={newSchedule.userID} onChange={handleInputChange} required />
                </div>
                <div>
                    <label>Device ID:</label>
                    <input type="text" name="deviceID" value={newSchedule.deviceID} onChange={handleInputChange} required />
                </div>
                <div>
                    <label>Start Time:</label>
                    <input type="time" name="startTime" value={newSchedule.startTime} onChange={handleInputChange} required />
                </div>
                <div>
                    <label>End Time:</label>
                    <input type="time" name="endTime" value={newSchedule.endTime} onChange={handleInputChange} required />
                </div>
                <div>
                    <label>Day of Week:</label>
                    <select name="dayOfWeek" value={newSchedule.dayOfWeek} onChange={handleInputChange} required>
                        <option value="">Select a day</option>
                        <option value="Monday">Monday</option>
                        <option value="Tuesday">Tuesday</option>
                        <option value="Wednesday">Wednesday</option>
                        <option value="Thursday">Thursday</option>
                        <option value="Friday">Friday</option>
                        <option value="Saturday">Saturday</option>
                        <option value="Sunday">Sunday</option>
                    </select>
                </div>
                <button type="submit">Add Schedule</button>
            </form>
            {loading ? <p><em>Loading...</em></p> : renderSchedulesTable(schedules)}
        </div>
    );
}

export default ScreenTimeSchedules;