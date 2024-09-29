import React from "react";


function PageHeader() {
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
                <button className="search-button">🔍</button>
            </div>
        </div>
    );
}

export default PageHeader;