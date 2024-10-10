import React from 'react';
import { useAtom } from 'jotai';
import { cartAtom, CartItem } from '../../atoms/CartAtom';
import { PaperAtom } from '../../atoms/PaperAtom';
import ConfirmOrderButton from '../../components/pages/ConfirmOrderButton';

const Cart = () => {
    const [cart, setCart] = useAtom(cartAtom);
    const [papers] = useAtom(PaperAtom);

    const updateQuantity = (product_id: number, quantity: number) => {
        if (quantity < 1) return;
        const updatedDtos = cart.dtos.map((item: CartItem) =>
            item.product_id === product_id ? { ...item, quantity } : item
        );
        setCart({ ...cart, dtos: updatedDtos });
    };
    

    const removeItem = (productId: number) => {
        setCart(prev => ({
            dtos: prev.dtos.filter(item => item.product_id !== productId)
        }));
    };
    const totalPrice = cart.dtos.reduce((sum, item) => {
        const product = papers.find(paper => paper.id === item.product_id);
        return sum + (product?.price ? product.price * item.quantity : 0);
    }, 0);

    return (
        <div className="cart-wrapper">
            <div className="cart-container">
                <h2 className="cart-header">Din indkøbskurv - ({cart.dtos.length})</h2>

                {cart.dtos.length === 0 ? (
                    <div className="empty-cart">Din kurv er tom.</div>
                ) : (
                    cart.dtos.map((item) => {
                        const product = papers.find(paper => paper.id === item.product_id);
                        if (!product) return null;

                        return (
                            <div key={item.product_id} className="cart-item">
                                <div className="cart-item-details">
                                    <h3 className="cart-item-name">{product.name}</h3>
                                    <p className="cart-item-price"> From {product.price}$ per. package </p>
                                </div>

                                <div className="quantity-controls">
                                    <div className="quantity-adjuster">
                                        <button
                                            onClick={() => updateQuantity(item.product_id, item.quantity - 1)}
                                            className="quantity-button"
                                        >
                                            −
                                        </button>
                                        <span className="quantity-display">{item.quantity}</span>
                                        <button
                                            onClick={() => updateQuantity(item.product_id, item.quantity + 1)}
                                            className="quantity-button"
                                        >
                                            +
                                        </button>
                                        
                                    </div>
                                    <div>
                                    <button
                                        onClick={() => removeItem(item.product_id)}
                                        className="remove-button"
                                        aria-label="Remove item"
                                    >
                                        ×
                                    </button>
                                </div>
                                </div>
                            </div>
                        );
                    })
                )}

                <div className="cart-footer">
                    <div className="cart-total">
                        <span className="total-price">Total Pris: {totalPrice} Dkk</span>
                        <span className="cart-count">Kurv: ({cart.dtos.length})</span>
                    </div>
                    <ConfirmOrderButton />
                </div>
            </div>
        </div>
    );
};

export default Cart;
