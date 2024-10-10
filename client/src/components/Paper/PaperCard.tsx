
import React, {useState} from "react";
import { useAtom } from 'jotai';
import { cartAtom, CartItem } from '../../atoms/CartAtom.tsx';
import {ResponseCreatePaperDTO, Properties, Paper, Api} from '../../Api.ts';


interface ProductCardProps {
    paper: ResponseCreatePaperDTO;

}

function PaperCard({ paper }: { paper: Paper }) {
    const [cart, setCart] = useAtom(cartAtom);
    const [count, setCount] = React.useState(1);
    const [restockAmount, setRestockAmount] = useState(0);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [stock, setStock] = useState(paper.stock);
    const [showRestock, setShowRestock] = useState(false);

    const api = new Api();


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
    const restockProduct = async () => {
        if (restockAmount > 0) {
            setLoading(true);
            setError(null);

            const newStock = (stock || 0) + restockAmount;

            const updateData = {
                name: paper.name,
                stock: newStock, // Opdater lagerbeholdning
                price: paper.price,
                discontinued: paper.discontinued,
                propertyIds: paper.properties && paper.properties.map(p => p?.id).filter(id => id !== undefined),
            };

            try {

                await api.api.paperUpdatePaper(paper.id || 0, updateData);
                setStock(newStock); // Opdater lokal stock-state
                setRestockAmount(0); // Nulstil restock-mængde
                setShowRestock(false);
            } catch (err) {
                setError('Failed to update stock. Please try again.');
            } finally {
                setLoading(false);
            }
        }
    };

    return (
        <div className="product-card">
            <h2>{paper.name}</h2>
            <p>{paper.stock && paper.stock > 0 ? (
                <p>{paper.stock} In Stock</p>) : (<p>Out of Stock</p>)}</p>
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

            <button onClick={() => setShowRestock(!showRestock)}>
                {showRestock ? "Cancel" : "Restock Product"}
            </button>


            {showRestock && (
                <div className="restock-control">
                    <input
                        type="number"
                        value={restockAmount}
                        onChange={(e) => setRestockAmount(Number(e.target.value))}
                        placeholder="Enter restock amount"
                    />
                    <button onClick={restockProduct} disabled={loading}>
                        {loading ? "Restocking..." : "Confirm Restock"}
                    </button>
                    {error && <p className="error-message">{error}</p>}
                </div>
            )}
        </div>
    );
}

export default PaperCard;
