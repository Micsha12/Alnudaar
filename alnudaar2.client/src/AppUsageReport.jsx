import React, { useState, useEffect } from 'react';
import './AppUsageReport.css'; // We'll create this CSS file next
import { useAuth } from './AuthContext'; // Assuming AuthContext provides selectedDevice and user

function AppUsageReport() {
    const { auth, selectedDevice } = useAuth();
    const user = auth?.user; // UserID might be useful for context, though selectedDevice is primary filter
    const [usageData, setUsageData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        if (!selectedDevice) {
            setError('No device selected. Please select a device to view reports.');
            setLoading(false);
            setUsageData([]); // Clear any previous data
            return;
        }

        const fetchAppUsage = async () => {
            setLoading(true);
            setError(null);
            try {
                // Adjust the API endpoint as needed.
                // Assuming the API can filter by deviceID.
                // If UserID is also required for filtering, add it: &userID=${user.userID}
                const response = await fetch(`https://localhost:7200/api/appusage?deviceID=${selectedDevice.deviceID}`);
                if (response.ok) {
                    const data = await response.json();
                    // Assuming data is an array of objects:
                    // { userID, deviceID, appName, timespan, usageDuration }
                    setUsageData(data);
                } else {
                    const errorText = await response.text();
                    console.error('Failed to fetch app usage data:', errorText);
                    setError(`Failed to fetch app usage data: ${response.statusText}`);
                }
            } catch (err) {
                console.error('Error fetching app usage data:', err);
                setError(`Error fetching app usage data: ${err.message}`);
            } finally {
                setLoading(false);
            }
        };

        fetchAppUsage();
    }, [selectedDevice, user]); // Re-fetch if selectedDevice or user changes

    const formatDuration = (durationString) => {
        // Assuming durationString is something like "PT1H30M5S" (ISO 8601 duration)
        // or seconds. This is a placeholder; adjust based on your actual duration format.
        // For simplicity, if it's just a string like "1 hour 30 minutes", return as is.
        // If it's in seconds, you can format it:
        if (!isNaN(durationString)) { // if it's a number (seconds)
            const totalSeconds = parseInt(durationString, 10);
            const hours = Math.floor(totalSeconds / 3600);
            const minutes = Math.floor((totalSeconds % 3600) / 60);
            const seconds = totalSeconds % 60;
            let formatted = '';
            if (hours > 0) formatted += `${hours}h `;
            if (minutes > 0) formatted += `${minutes}m `;
            if (seconds > 0 || (hours === 0 && minutes === 0)) formatted += `${seconds}s`;
            return formatted.trim();
        }
        return durationString; // Fallback if not a simple number
    };
    
    const formatDate = (dateString) => {
        // Assuming timespan is an ISO date string
        if (!dateString) return 'N/A';
        try {
            return new Date(dateString).toLocaleDateString(undefined, {
                year: 'numeric', month: 'long', day: 'numeric',
                hour: '2-digit', minute: '2-digit'
            });
        } catch (e) {
            return dateString; // Fallback if not a valid date
        }
    };


    if (!selectedDevice) {
        return (
            <div className="app-usage-container">
                <h1>App Usage Report</h1>
                <p className="app-usage-message">Please select a device to view its app usage report.</p>
            </div>
        );
    }

    if (loading) {
        return (
            <div className="app-usage-container">
                <h1>App Usage Report for {selectedDevice.deviceName || selectedDevice.deviceID}</h1>
                <p className="app-usage-message">Loading app usage data...</p>
            </div>
        );
    }

    if (error) {
        return (
            <div className="app-usage-container">
                <h1>App Usage Report for {selectedDevice.deviceName || selectedDevice.deviceID}</h1>
                <p className="app-usage-message error-message">{error}</p>
            </div>
        );
    }

    if (usageData.length === 0) {
        return (
            <div className="app-usage-container">
                <h1>App Usage Report for {selectedDevice.deviceName || selectedDevice.deviceID}</h1>
                <p className="app-usage-message">No app usage data available for this device.</p>
            </div>
        );
    }

    return (
        <div className="app-usage-container">
            <h1>App Usage Report for {selectedDevice.deviceName || selectedDevice.deviceID}</h1>
            <div className="app-usage-table-wrapper">
                <table className="app-usage-table">
                    <thead>
                        <tr>
                            <th>User ID</th>
                            <th>Device ID</th>
                            <th>App Name</th>
                            <th>Timespan</th>
                            <th>Usage Duration</th>
                        </tr>
                    </thead>
                    <tbody>
                        {usageData.map((entry, index) => (
                            // It's better to have a unique ID from the backend for the key
                            <tr key={entry.id || `${entry.deviceID}-${entry.appName}-${entry.timespan}-${index}`}>
                                <td>{entry.userID}</td>
                                <td>{entry.deviceID}</td>
                                <td>{entry.appName}</td>
                                <td>{formatDate(entry.timespan)}</td>
                                <td>{formatDuration(entry.usageDuration)}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
}

export default AppUsageReport;