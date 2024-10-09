import React from 'react';
import { useAtom } from 'jotai';
import { cartAtom } from '../../atoms/CartAtom'; // Importér din cartAtom
import { Api } from '../../Api'; // Importér din API klasse

const ConfirmOrderButton = () => {
    const [cart] = useAtom(cartAtom);
    const api = new Api();

    const handleConfirmOrder = async () => {
        const orderEntries = cart.dtos.map(item => ({
            quantity: item.quantity,
            productId: item.product_id,
        }));

        const orderData = {
            dtos: orderEntries,
            orderDate: new Date().toISOString(),
            status: 'Confirmed',
            totalAmount: cart.dtos.reduce((total, item) => total + item.product_id * item.quantity, 0),
            customerId: 1, // Brug en relevant customerId
        };

        try {
            const response = await api.api.orderCreateOrder(orderData);
            console.log('Order created successfully:', response);
            // Her kan du tilføje logik for at opdatere UI eller rydde kurven
        } catch (error) {
            console.error('Error creating order:', error);
        }
    };

    return (
        <button onClick={handleConfirmOrder}>
            Confirm Order
        </button>
    );
};

export default ConfirmOrderButton;
