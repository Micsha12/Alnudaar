import React, { useEffect, useState } from 'react';

function ScreenTimeSchedules() {
    const [schedules, setSchedules] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetchScreenTimeSchedules();
    }, []);

    const fetchScreenTimeSchedules = async () => {
        const response = await fetch('/api/screentimeschedule');
        const data = await response.json();
        setSchedules(data);
        setLoading(false);
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
            <p>This component demonstrates fetching data from the server.</p>
            {loading ? <p><em>Loading...</em></p> : renderSchedulesTable(schedules)}
        </div>
    );
}

export default ScreenTimeSchedules;