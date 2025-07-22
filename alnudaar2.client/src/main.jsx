import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from './App';
import { AuthProvider } from './AuthContext';
import './index.css';

const rootElement = document.getElementById('root');
const root = createRoot(rootElement);

root.render(
    <StrictMode>
        <AuthProvider>
            <App />
        </AuthProvider>
    </StrictMode>
);