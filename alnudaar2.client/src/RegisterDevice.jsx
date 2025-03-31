import React, { useState } from 'react';
import './RegisterDevice.css';
import { useAuth } from './AuthContext';

function RegisterDevice() {
    const [device, setDevice] = useState({
        name: ''
    });
    const { auth } = useAuth();

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setDevice({ ...device, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const response = await fetch('https://localhost:7200/api/devices/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ ...device, userID: auth.user.userID })
        });
        if (response.ok) {
            // Handle successful registration (e.g., redirect to dashboard)
            console.log('Device registered successfully');
        } else {
            console.error('Failed to register device');
        }
    };

    return (
        <div className="register-device-container">
            <h1>Register Device</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Device Name:</label>
                    <input type="text" name="name" value={device.name} onChange={handleInputChange} required />
                </div>
                <button type="submit">Register Device</button>
            </form>
        </div>
    );
}

export default RegisterDevice;