import React, { createContext, useState, useContext } from 'react';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [auth, setAuth] = useState(() => {
        const storedAuth = localStorage.getItem('auth');
        return storedAuth ? JSON.parse(storedAuth) : { isAuthenticated: false, user: null };
    });

    const [selectedDevice, setSelectedDevice] = useState(null); // Add selectedDevice state

    const login = (user) => {
        const authState = { isAuthenticated: true, user };
        setAuth(authState);
        localStorage.setItem('auth', JSON.stringify(authState));
    };

    const logout = () => {
        const authState = { isAuthenticated: false, user: null };
        setAuth(authState);
        localStorage.removeItem('auth');
        setSelectedDevice(null); // Clear selected device on logout
    };

    return (
        <AuthContext.Provider value={{ auth, login, logout, selectedDevice, setSelectedDevice }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);