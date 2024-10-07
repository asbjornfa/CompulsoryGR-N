import { RequestCreatePaperDTO } from "../../Api.ts";
import React from "react";
import { useAtom } from 'jotai';
import { cartAtom, CartItem } from '../../atoms/CartAtom.tsx';
import { ResponseCreatePaperDTO } from '../../Api.ts';

interface ProductCardProps {
    paper: ResponseCreatePaperDTO;
}

function PaperCard({ paper }: ProductCardProps) {
    const [cart, setCart] = useAtom(cartAtom);
    const [count, setCount] = React.useState(1);

    const increment = () => {
        setCount(prev => prev + 1);
    };

    const decrement = () => {
        setCount(prev => (prev > 1 ? prev - 1 : 1));
    };

    const addToCart = () => {
        // Opretter et nyt cart item
        const newItem: CartItem = {
            product_id: paper.id || 0, // Bruger paper.id, men sætter en default værdi
            quantity: count,
            count
        };

        // Opdaterer cart
        setCart(prev => {
            const existingItemIndex = prev.findIndex(item => item.product_id === newItem.product_id);

            if (existingItemIndex !== -1) {
                // Hvis varen allerede findes, opdaterer vi den
                const updatedCart = [...prev];
                updatedCart[existingItemIndex].quantity += count; // Opdaterer mængden
                updatedCart[existingItemIndex].count = count; // Opdaterer count
                return updatedCart;
            }

            // Hvis varen ikke findes, tilføjer vi den
            return [...prev, newItem];
        });

        setCount(1); // Resetter tælleren til 1
    };

    return (
        <div className="product-card">
            <h2>{paper.name}</h2>
            <p>{paper.stock ? "In Stock" : "Out of Stock"}</p>
            <p>{paper.price + "$"}</p>
            <div className="quantity-control">
                <button onClick={decrement}>-</button>
                <span>{count}</span>
                <button onClick={increment}>+</button>
            </div>
            <button className="add-to-cart" onClick={addToCart}>Add to Cart</button>
        </div>
    );
}

export default PaperCard;
