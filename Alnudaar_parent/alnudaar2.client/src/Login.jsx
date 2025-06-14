import React, { useState } from 'react';
import './Login.css';
import { useAuth } from './AuthContext';
import { useNavigate } from 'react-router-dom';

function Login() {
    const [credentials, setCredentials] = useState({
        username: '',
        password: ''
    });
    const { login } = useAuth();
    const navigate = useNavigate();

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setCredentials({ ...credentials, [name]: value });
    };

    const [error, setError] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        if (!credentials.username || !credentials.password) {
            setError('Please fill in all fields.');
            return;
        }
        // If you use email as username, add email format check:
        // if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(credentials.username)) {
        //     setError('Please enter a valid email address.');
        //     return;
        // }
        const response = await fetch('https://localhost:7200/api/users/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(credentials)
        });
        if (response.ok) {
            const user = await response.json();
            login(user);
            navigate('/schedules');
        } else {
            setError('Failed to login. Please check your credentials.');
        }
    };

    return (
        <div className="login-container">
            <h1>Login</h1>
            {error && <div className="login-error">{error}</div>}
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Username:</label>
                    <input type="text" name="username" value={credentials.username} onChange={handleInputChange} required />
                </div>
                <div>
                    <label>Password:</label>
                    <input type="password" name="password" value={credentials.password} onChange={handleInputChange} required />
                </div>
                <button type="submit">Login</button>
            </form>
        </div>
    );
}

export default Login;