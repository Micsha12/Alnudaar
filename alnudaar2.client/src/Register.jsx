import React, { useState } from 'react';

function Register() {
    const [user, setUser] = useState({
        username: '',
        password: '',
        email: '',
        userType: 'Parent'
    });

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setUser({ ...user, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const response = await fetch('https://localhost:7200/api/users/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });
        if (response.ok) {
            // Handle successful registration (e.g., redirect to login page)
            console.log('User registered successfully');
        } else {
            console.error('Failed to register user');
        }
    };

    return (
        <div>
            <h1>Register</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Username:</label>
                    <input type="text" name="username" value={user.username} onChange={handleInputChange} required />
                </div>
                <div>
                    <label>Password:</label>
                    <input type="password" name="password" value={user.password} onChange={handleInputChange} required />
                </div>
                <div>
                    <label>Email:</label>
                    <input type="email" name="email" value={user.email} onChange={handleInputChange} required />
                </div>
                <button type="submit">Register</button>
            </form>
        </div>
    );
}

export default Register;