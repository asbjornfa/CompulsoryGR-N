import React from "react";
import { useAtom } from "jotai";
import { namePaperAtom, stockPaperAtom, pricePaperAtom } from "../atoms/PaperAtom.tsx";
import { useInitializeData } from "../useInitializeData.ts";

const Home = () => {
    // Using Jotai atoms to manage paper state
    const [paperName, setPaperName] = useAtom(namePaperAtom);
    const [paperStock, setPaperStock] = useAtom(stockPaperAtom);
    const [paperPrice, setPaperPrice] = useAtom(pricePaperAtom);

    // Initialize data when the component mounts
    useInitializeData();

    // Function to handle paper creation
    const handleCreatePaper = async () => {
        const createPaperDTO = {
            name: paperName,
            stock: paperStock,
            price: paperPrice,
        };

        try {
            const response = await fetch('http://localhost:5000/api/paper', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(createPaperDTO),
            });

            if (!response.ok) {
                throw new Error("Failed to create paper");
            }

            const result = await response.json();
            console.log("Paper created", result);
            // Optionally, reset the form fields
            setPaperName('');
            setPaperStock(0);
            setPaperPrice(0);
        } catch (error) {
            console.error("Error!", error);
        }
    };

    return (
        <div className="container">
            <h1 className="title">Create Paper</h1>
            <div className="form-group">
                <input
                    type="text"
                    placeholder="Paper Name"
                    value={paperName}
                    onChange={(e) => setPaperName(e.target.value)}
                    className="input-field"
                />
                <input
                    type="number"
                    placeholder="Stock"
                    value={paperStock}
                    onChange={(e) => setPaperStock(parseInt(e.target.value) || 0)}
                    className="input-field"
                />
                <input
                    type="number"
                    placeholder="Price"
                    value={paperPrice}
                    onChange={(e) => setPaperPrice(parseFloat(e.target.value) || 0)}
                    className="input-field"
                />
                <button className="btn" type="button" onClick={handleCreatePaper}>
                    Create Paper
                </button>
            </div>
        </div>
    );
};

export default Home;
