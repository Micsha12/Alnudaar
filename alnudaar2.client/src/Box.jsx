import React, { useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";
import './Box.css';

function Box({name, route}) {
    const navigate = useNavigate();
    return (
        <div className="box" onClick={() => navigate(route)}>
            <h2>{name}</h2>
        </div>
    );
} 

export default Box;