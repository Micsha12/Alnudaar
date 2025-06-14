import React, { useState, useEffect } from 'react';
import './ManageDevices.css';
import { useAuth } from './AuthContext';

function ManageDevices() {
    const [devices, setDevices] = useState([]);
    const { auth } = useAuth();

    useEffect(() => {
        const fetchDevices = async () => {
            const response = await fetch(`https://localhost:7200/api/devices/user/${auth.user.userID}`);
            const data = await response.json();
            setDevices(data);
        };

        fetchDevices();
    }, [auth.user.userID]);

    const handleDelete = async (deviceID) => {
        const response = await fetch(`https://localhost:7200/api/devices/${deviceID}`, {
            method: 'DELETE'
        });
        if (response.ok) {
            setDevices(devices.filter(device => device.deviceID !== deviceID));
        } else {
            console.error('Failed to delete device');
        }
    };

    const handleNameChange = async (deviceID, newName) => {
        const response = await fetch(`https://localhost:7200/api/devices/${deviceID}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ deviceID, name: newName })
        });
        if (response.ok) {
            setDevices(devices.map(device => device.deviceID === deviceID ? { ...device, name: newName } : device));
        } else {
            console.error('Failed to update device name');
        }
    };

    return (
        <div className="manage-devices-container">
            <h1>Manage Devices</h1>
            <ul>
                {devices.map(device => (
                    <li key={device.deviceID}>
                        <span>{device.name} (ID: {device.deviceID})</span>
                        <button onClick={() => handleDelete(device.deviceID)}>Delete</button>
                        <button onClick={() => {
                            const newName = prompt('Enter new name:', device.name);
                            if (newName) {
                                handleNameChange(device.deviceID, newName);
                            }
                        }}>Change Name</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default ManageDevices;