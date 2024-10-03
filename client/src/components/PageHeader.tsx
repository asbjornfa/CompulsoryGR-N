import React from "react";
import { useNavigate } from "react-router-dom";



function PageHeader() {
    const navigate = useNavigate(); // Brug useNavigate

    // Funktion der håndterer navigation
    const handleCreateClick = () => {
        navigate("/createPaper"); // Naviger til "/createPaper"
    };

    return (
        <div className="header">
                <div className="logo">
                    <img src="/path-to-logo.png" alt="JA Paper Logo"/>
                    <span className="logo-text">JA Paper</span>
                </div>
                <nav className="nav">
                    <ul>
                        <li><a href="/">Home</a></li>
                        <li><a href="/Shop">Shop</a></li>
                        <li><a href="/about-us">About us</a></li>
                    </ul>
                </nav>
                <div className="search">
                    <button className="create-button" type="button" onClick={handleCreateClick}>+</button>
                </div>
            </div>
            );
            }

            export default PageHeader;