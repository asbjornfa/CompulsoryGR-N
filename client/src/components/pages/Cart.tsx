import React from 'react';
import { useAtom } from 'jotai';
import { cartAtom } from '../../atoms/CartAtom.tsx';
import {http} from '../../http.ts'

function Cart() {
    const [cart] = useAtom(cartAtom); // get the cart state

    return (
        <div className="cart-container">
            {cart.dtos!.length === 0 ? (
                <p>Your cart is empty</p>
            ) : (
                cart.dtos!.map((item) => (
                    <div key={item.productId} className="cart-item">
                        <h3>Product ID: {item.productId}</h3>
                        <p>Quantity: {item.quantity}</p>
                    </div>
                ))
            )}
            <button onClick={e => {
                http.api.orderCreateOrder(cart).then(result => {
                    //Response
                })
            }}>Send</button>
            
        </div>
    );
}

export default Cart;
