import React, { useEffect, useState } from 'react';
import { useAtom } from 'jotai';
import { OrderAtom} from "../../atoms/OrderAtom.tsx";
import {Api, Order, RequestCreateOrderDTO} from '../../Api.ts';
import { ResponseCreateOrderDTO } from '../../Api.ts'; // Dit DTO-interface

const OrderHistoryAdmin = () => {
    const [orders, setOrders] = useAtom(OrderAtom); // Hent ordrer fra Jotai state
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const [customerNames, setCustomerNames] = useState<{ [key: number]: string }>({});
    const api = new Api();

    useEffect(() => {
        const fetchOrders = async () => {
            try {
                const response = await api.api.orderGetOrders();
                const orders = response.data as Order[];
                setOrders(orders);

                // Fetch customer names in parallel
                const customerPromises = orders.map(async (order: Order) => {
                    const customerId = order.customerId as number; // Type assertion
                    const customerResponse = await api.api.customerGetCustomerById(customerId);
                    return { customerId, name: customerResponse.data.name };
                });

                // Wait for all customer names to be fetched
                const customerData = await Promise.all(customerPromises);

                // Create a map of customerId -> customerName
                const customerNameMap: { [key: number]: string } = {};
                customerData.forEach(({ customerId, name }) => {
                    if (name != null) {
                        customerNameMap[customerId] = name;
                    }
                });
                setCustomerNames(customerNameMap);
            } catch (err) {
                setError('Fejl ved hentning af ordrer.');
            } finally {
                setLoading(false);
            }
        };

        fetchOrders();
    }, [setOrders]);

    const handleStatusChange = async (orderId: number | undefined, newStatus: string) => {
        try {
            const updateData = {
                status: newStatus
            };
            await api.api.orderUpdateOrder(orderId, updateData);

            setOrders(prevOrders =>
                prevOrders.map(order =>
                    order.id === orderId ? { ...order, status: newStatus } : order
                )
            );
            console.log(`Order ${orderId} updated to status: ${newStatus}`);
        } catch (err) {
            console.error('Fejl ved opdatering af status:', err);
        }
    };

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
                            <td className="px-4 py-2 border">
                                <select
                                    value={order.status}
                                    onChange={(e) => handleStatusChange(order.id, e.target.value)}
                                    className="border rounded p-2 w-40"
                                >
                                    <option value="Pending">Pending</option>
                                    <option value="Processing">Processing</option>
                                    <option value="Shipped">Shipped</option>
                                    <option value="Delivered">Delivered</option>
                                    <option value="Cancelled">Cancelled</option>
                                </select>
                            </td>
                                <td className="px-4 py-2 border">{order.totalAmount}</td>
                                <td className="px-4 py-2 border">
                                    {customerNames[order.customerId ?? 0]}
                                </td>
                        </tr>
                        ))}
                    </tbody>
                </table>
                )}
        </div>
    );
};

export default OrderHistoryAdmin;