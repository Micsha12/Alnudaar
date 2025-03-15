import React, { useState } from 'react';

function RegisterDevice() {
    const [device, setDevice] = useState({
        userID: '',
        deviceName: ''
    });

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setDevice({ ...device, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const response = await fetch('/api/devices/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(device)
        });
        if (response.ok) {
            // Handle successful registration (e.g., redirect to dashboard)
        }
    };

    return (
        <div>
            <h1>Register Device</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>User ID:</label>
                    <input type="text" name="userID" value={device.userID} onChange={handleInputChange} required />
                </div>
                <div>
                    <label>Device Name:</label>
                    <input type="text" name="deviceName" value={device.deviceName} onChange={handleInputChange} required />
                </div>
                <button type="submit">Register Device</button>
            </form>
        </div>
    );
}

export default RegisterDevice;