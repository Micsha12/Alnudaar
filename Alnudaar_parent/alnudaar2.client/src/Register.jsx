import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './Register.css';

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

    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        const response = await fetch('https://localhost:7200/api/users/register', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(user)
        });

        if (response.ok) {
            // Registration successful, redirect to login page
            navigate('/login');
        } else {
            const errorMsg = await response.text();
            setError(errorMsg || 'Registration failed.');
        }
    };

    return (
        <div className="register-container">
            <h1>Register</h1>
            {error && <div className="register-error">{error}</div>}
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