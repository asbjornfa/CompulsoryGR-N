
import React from "react";
import { useAtom } from 'jotai';
import { cartAtom, CartItem } from '../../atoms/CartAtom.tsx';
import {ResponseCreatePaperDTO, Properties, Paper} from '../../Api.ts';


interface ProductCardProps {
    paper: ResponseCreatePaperDTO;

}

function PaperCard({ paper }: { paper: Paper }) {
    const [cart, setCart] = useAtom(cartAtom);
    const [count, setCount] = React.useState(1);

    console.log('Paper properties:', paper.properties);

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
        };





        // Opdaterer cart
        setCart(prev => {
            const existingItemIndex = prev.dtos.findIndex(item => item.product_id === newItem.product_id);

            if (existingItemIndex !== -1) {
                // Hvis varen allerede findes, opdaterer vi den
                const updatedCart = [...prev.dtos];
                updatedCart[existingItemIndex].quantity += count; // Opdaterer mængden
                return { dtos: updatedCart }; // Retur til det korrekte format
            }

            // Hvis varen ikke findes, tilføjer vi den
            return { dtos: [...prev.dtos, newItem] }; // Retur til det korrekte format
        });

        setCount(1); // Resetter tælleren til 1
    };

    const updateStock = () => {


    }

    return (
        <div className="product-card">
            <h2>{paper.name}</h2>
            <p>{paper.stock && paper.stock > 0 ? (
                <p>{paper.stock} In Stock</p>) : ( <p>Out of Stock</p>)}</p>
            <p>{paper.price + "$"}</p>

            <div className="product-tags">
                {paper.properties?.map((property: Properties, index: number) => (
                    <span key={index} className="tag">{property.propertyName}</span>
                    ))}
            </div>
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
