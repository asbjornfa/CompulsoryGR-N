import React from 'react';
import { useAtom } from 'jotai';
import { cartAtom } from '../../atoms/CartAtom.tsx';

function Cart() {
    const [cart] = useAtom(cartAtom); // get the cart state

    return (
        <div className="cart-container">
            {cart.length === 0 ? (
                <p>Your cart is empty</p>
            ) : (
                cart.map((item) => (
                    <div key={item.product_id} className="cart-item">
                        <h3>Product ID: {item.product_id}</h3>
                        <p>Quantity: {item.quantity}</p>
                        <p>Count: {item.count}</p>
                    </div>
                ))
            )}
        </div>
    );
}

export default Cart;
