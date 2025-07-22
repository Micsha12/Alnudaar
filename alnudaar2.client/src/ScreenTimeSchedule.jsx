import React, { useState, useEffect } from 'react';
import './ScreenTimeSchedule.css';
import { useAuth } from './AuthContext';

function ScreenTimeSchedules() {
    const { auth, selectedDevice } = useAuth();
    const user = auth?.user;
    const [schedules, setSchedules] = useState([]);
    const [editingDay, setEditingDay] = useState(null);
    const daysOfWeek = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'];

    useEffect(() => {
        if (!selectedDevice) {
            console.error('No device selected');
            return;
        }

        // Fetch schedules for the selected device
        const fetchSchedules = async () => {
            const response = await fetch(`https://localhost:7200/api/screentimeschedule?deviceID=${selectedDevice.deviceID}`);
            if (response.ok) {
                const data = await response.json();
                setSchedules(data);
            } else {
                console.error('Failed to fetch schedules');
            }
        };

        fetchSchedules();
    }, [selectedDevice]);

    const handleInputChange = (day, field, value) => {
        setSchedules((prevSchedules) => {
            const updatedSchedules = [...prevSchedules];
            // Find by dayOfWeek AND deviceID
            const scheduleIndex = updatedSchedules.findIndex(
                (s) => s.dayOfWeek === day && s.deviceID === selectedDevice.deviceID
            );

            if (scheduleIndex !== -1) {
                updatedSchedules[scheduleIndex][field] = value;
            } else {
                updatedSchedules.push({
                    dayOfWeek: day,
                    startTime: field === 'startTime' ? value : '',
                    endTime: field === 'endTime' ? value : '',
                    deviceID: selectedDevice.deviceID,
                    userID: user.userID,
                });
            }

            return updatedSchedules;
        });
    };

    const handleSave = async (day) => {
        if (!user || !selectedDevice) {
            console.error('User or device is not selected');
            return;
        }

        // Find the schedule for this device and day
        const schedule = schedules.find(
            (s) => s.dayOfWeek === day && s.deviceID === selectedDevice.deviceID
        );
        if (schedule) {
            const payload = {
                ...schedule,
                userID: user.userID,
                deviceID: selectedDevice.deviceID,
            };

            const response = await fetch('https://localhost:7200/api/screentimeschedule', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(payload),
            });

            if (response.ok) {
                console.log(`Schedule for ${day} saved successfully`);
            } else {
                console.error(`Failed to save schedule for ${day}`);
            }
        }
        setEditingDay(null);
    };
    return (
        <div className="schedules-container">
            <h1>Weekly Screen Time Schedules</h1>
            {daysOfWeek.map((day) => {
                const schedule = schedules.find(
    (s) => s.dayOfWeek === day && s.deviceID === selectedDevice.deviceID) || { startTime: '', endTime: '' };

                return (
                    <div key={day} className="schedule-row">
                        <h3>{day}</h3>
                        {editingDay === day ? (
                            <>
                                <label>Start Time:</label>
                                <input
                                    type="time"
                                    value={schedule.startTime}
                                    onChange={(e) => handleInputChange(day, 'startTime', e.target.value)}
                                />
                                <label>End Time:</label>
                                <input
                                    type="time"
                                    value={schedule.endTime}
                                    onChange={(e) => handleInputChange(day, 'endTime', e.target.value)}
                                />
                                <button onClick={() => handleSave(day)}>Save</button>
                            </>
                        ) : (
                            <>
                                <span>
                                    {schedule.startTime || 'Not Set'} - {schedule.endTime || 'Not Set'}
                                </span>
                                <button onClick={() => setEditingDay(day)}>Adjust Times</button>
                            </>
                        )}
                    </div>
                );
            })}
        </div>
    );
}

export default ScreenTimeSchedules;