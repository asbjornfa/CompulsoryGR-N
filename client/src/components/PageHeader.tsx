import React from "react";
import { useNavigate, Link } from "react-router-dom";
import { useAtom } from 'jotai';
import { cartAtom } from '../../src/atoms/CartAtom';

function PageHeader() {
    const navigate = useNavigate();
    const [cart] = useAtom(cartAtom);

    const handleCreateClick = () => {
        navigate("/createPaper");
    };

    return (
        <div className="header">
            <div className="logo">
                <img src="/src/assets/logo.png" alt="JA Paper Logo" style={{ width: '70px', height: 'auto' }} />
                <span className="logo-text">JA Paper</span>
            </div>
            <nav className="nav">
                <ul>
                    <li><Link to="/">Home</Link></li>
                    <li><Link to="/Shop">Shop</Link></li>
                    <li><Link to="/about-us">About us</Link></li>
                    <li><Link to="/OrderHistory">Order History</Link></li>
                </ul>
            </nav>
            <div className="search" style={{ display: 'flex', alignItems: 'center' }}>
                <Link to="/cart" className="cart-button" style={{ marginRight: '15px', position: 'relative' }}>
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="size-6">
                        <path strokeLinecap="round" strokeLinejoin="round" d="M15.75 10.5V6a3.75 3.75 0 1 0-7.5 0v4.5m11.356-1.993 1.263 12c.07.665-.45 1.243-1.119 1.243H4.25a1.125 1.125 0 0 1-1.12-1.243l1.264-12A1.125 1.125 0 0 1 5.513 7.5h12.974c.576 0 1.059.435 1.119 1.007ZM8.625 10.5a.375.375 0 1 1-.75 0 .375.375 0 0 1 .75 0Zm7.5 0a.375.375 0 1 1-.75 0 .375.375 0 0 1 .75 0Z" />
                    </svg>
                </Link>
                <button className="create-button" type="button" onClick={handleCreateClick}>+</button>
            </div>
        </div>
    );
}

export default PageHeader;
