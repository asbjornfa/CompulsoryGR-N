import React, { useEffect, useState } from 'react';
import { useAtom } from 'jotai';
import { OrderAtom} from "../../atoms/OrderAtom.tsx";
import {Api, Order} from '../../Api.ts';
import { ResponseCreateOrderDTO } from '../../Api.ts'; // Dit DTO-interface

const OrderHistoryAdmin = () => {
    const [orders, setOrders] = useAtom(OrderAtom); // Hent ordrer fra Jotai state
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const api = new Api();

    useEffect(() => {
        const fetchOrders = async () => {
            try {
                const response = await api.api.orderGetOrders();
                const orders = response.data as Order[];
                setOrders(orders);
            } catch (err) {
                setError('Fejl ved hentning af ordrer.');
            } finally {
                setLoading(false);
            }
        };

        fetchOrders();
    }, [setOrders]);

    if (loading) return <p>Indlæser ordrer...</p>;
    if (error) return <p>{error}</p>;

    return (
        <div className="p-4">
            <h1 className="text-2xl font-bold mb-4">Order History</h1>
            {orders.length === 0 ? (
                <p>Ingen ordrer fundet.</p>
            ) : (
                <table className="min-w-full table-auto bg-white border border-gray-300">
                    <thead>
                    <tr className="bg-gray-200 text-left">
                        <th className="px-4 py-2 border">Order ID</th>
                        <th className="px-4 py-2 border">Order Date</th>
                        <th className="px-4 py-2 border">Delivery Date</th>
                        <th className="px-4 py-2 border">Status</th>
                        <th className="px-4 py-2 border">Total Amount</th>
                        <th className="px-4 py-2 border">Customer ID</th>
                    </tr>
                    </thead>
                    <tbody>
                    {orders.map((order: ResponseCreateOrderDTO) => (
                        <tr key={order.id} className="border-t">
                            <td className="px-4 py-2 border">{order.id}</td>
                            <td className="px-4 py-2 border">
                                {new Date(order.orderDate ?? '').toLocaleDateString()}
                            </td>
                            <td className="px-4 py-2 border">
                                {new Date(order.deliveryDate ?? '').toLocaleDateString()}
                            </td>
                            <td className="px-4 py-2 border">{order.status}</td>
                            <td className="px-4 py-2 border">{order.totalAmount}</td>
                            <td className="px-4 py-2 border">{order.customerId}</td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            )}
        </div>
    );
};

export default OrderHistoryAdmin;