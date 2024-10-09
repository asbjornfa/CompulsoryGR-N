import React from 'react';
import { useAtom } from 'jotai';
import { cartAtom } from '../../atoms/CartAtom';
import { PaperAtom } from '../../atoms/PaperAtom';

import ConfirmOrderButton from '../../components/pages/ConfirmOrderButton'; // Importér ConfirmOrderButton

const Cart = () => {
    const [cart, setCart] = useAtom(cartAtom); // Husk at få adgang til setCart
   // const [papers, setPapers] = useAtom(PaperAtom);



    // Funktion til at fjerne varer fra kurven
    const removeItem = (index: number) => {
        const updatedDtos = cart.dtos.filter((_, i) => i !== index); // Fjern varen fra kurven
        setCart({ ...cart, dtos: updatedDtos }); // Opdater cart atom med den nye liste
    };
    
   // const totalPrice = cart.dtos.reduce((acc, item) => acc + papers.price * item.quantity, 0);

    return (
        <div className="fixed top-16 right-0 p-6 bg-white rounded-lg shadow-md w-96 h-full overflow-y-auto">
            <h1 className="text-5xl m-5 text-center font-bold">Shopping Cart</h1>
            {cart.dtos && cart.dtos.length > 0 ? (
                <div>
                    <ul className="cart-list space-y-4">
                        {cart.dtos.map((item, index) => (
                            <li key={index} className="cart-item p-4 bg-lightGray rounded-lg shadow-sm flex justify-between items-center">
                                <div>
                                    <h2 className="text-2xl font-semibold">{item.quantity} x {item.product_id} </h2>
                                </div>
                                <button onClick={() => removeItem(index)} className="btn btn-danger bg-red-500 text-white px-4 py-2 rounded-lg hover:bg-red-700">
                                    Remove
                                </button>
                            </li>
                        ))}
                    </ul>
                    <ConfirmOrderButton /> {/* Placer ConfirmOrderButton her */}
                </div>
            ) : (
                <p className="text-xl text-center mt-6">Your cart is empty.</p>
            )}
        </div>
    );
};

export default Cart;

