import React, { useState, useEffect } from "react";
import "./AppUsageReport.css";
import { useAuth } from "./AuthContext";

function AppUsageReport() {
    const { auth, selectedDevice } = useAuth();
    const [usageData, setUsageData] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    useEffect(() => {
    if (!selectedDevice) {
        setUsageData([]);
        setError("No device selected. Please select a device to view reports.");
        setLoading(false);
        return;
    }

    const fetchUsage = async () => {
        setLoading(true);
        setError(null);
        try {
            const response = await fetch(`https://localhost:7200/api/appusagereport/device/${selectedDevice.deviceID}`);
            if (!response.ok) {
                let errorText = "";
                try {
                    errorText = (await response.json()).message;
                } catch {
                    errorText = await response.text();
                }
                throw new Error(errorText || `HTTP error ${response.status}`);
            }
            const data = await response.json();
            setUsageData(data);
        } catch (err) {
            setError(err.message || "Failed to fetch app usage data.");
            setUsageData([]);
        } finally {
            setLoading(false); // <-- Always set loading to false
        }
    };

        fetchUsage();
    }, [selectedDevice]);

    const formatDuration = (seconds) => {
        if (isNaN(seconds)) return seconds;
        const totalSeconds = parseInt(seconds, 10);
        const h = Math.floor(totalSeconds / 3600);
        const m = Math.floor((totalSeconds % 3600) / 60);
        const s = totalSeconds % 60;
        return [
            h > 0 ? `${h}h` : "",
            m > 0 ? `${m}m` : "",
            s > 0 || (h === 0 && m === 0) ? `${s}s` : "",
        ]
            .filter(Boolean)
            .join(" ");
    };

    const formatDate = (dateString) => {
        if (!dateString) return "N/A";
        try {
            return new Date(dateString).toLocaleString();
        } catch {
            return dateString;
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

    if (!usageData || usageData.length === 0) {
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
                            <th>Timestamp</th>
                            <th>Usage Duration</th>
                        </tr>
                    </thead>
                    <tbody>
                        {usageData.map((entry, idx) => (
                            <tr key={entry.appUsageReportID || `${entry.deviceID}-${entry.appName}-${entry.timestamp}-${idx}`}>
                                <td>{entry.userID}</td>
                                <td>{entry.deviceID}</td>
                                <td>{entry.appName}</td>
                                <td>{formatDate(entry.timestamp)}</td>
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