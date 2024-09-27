import React, { useEffect } from "react";
import { useAtom } from "jotai";
import { namePaperAtom, stockPaperAtom, pricePaperAtom, PaperAtom } from "../atoms/PaperAtom.tsx";

export default function Home() {
    const [paperName, setPaperName] = useAtom(namePaperAtom);
    const [paperStock, setPaperStock] = useAtom(stockPaperAtom);
    const [paperPrice, setPaperPrice] = useAtom(pricePaperAtom);
    const [papers, setPapers] = useAtom(PaperAtom);

    useEffect(() => {
        const fetchPapers = async () => {
            const response = await fetch('http://localhost:5000/api/paper');
            const data = await response.json();
            setPapers(data);
        };

        fetchPapers();
    }, [setPapers]);

    const handleCreatePaper = async () => {
        const createPaperDTO = {name: paperName, stock: paperStock, price: paperPrice};

        try {
            const response = await fetch('http://localhost:5000/api/paper', {
                method: 'POST',
                headers: {'Content-Type': 'application/json'},
                body: JSON.stringify(createPaperDTO),
            });

            if (response.ok) {
                const newPaper = await response.json();
                setPapers((prev) => [...prev, newPaper]); // Opdater listen med det nye papir
            }
        } catch (error) {
            console.error("Error!", error);
        }
    };

    return (
        <div className="create-paper-container">
            <h2 className="create-paper-title">Create Paper</h2>

            <div className="input-group">
                <input type="text" value={paperName} onChange={(e) => setPaperName(e.target.value)}
                       placeholder="Paper Name" className="input-field"/>
                <input type="number" value={paperStock} onChange={(e) => setPaperStock(parseInt(e.target.value))}
                       placeholder="Stock" className="input-field"/>
                <input type="number" value={paperPrice} onChange={(e) => setPaperPrice(parseFloat(e.target.value))}
                       placeholder="Price" className="input-field"/>
            </div>
            <button onClick={handleCreatePaper} className="create-button">Create Paper</button>
            <ul className="list-disc pl-5">
                {papers.map((paper, index) => (
                    <li key={index} className="mt-2 text-gray-700">
                        {paper.name} - {paper.stock} in stock -
                        ${paper.price !== undefined ? paper.price.toFixed(2) : 'N/A'}
                    </li>
                ))}
            </ul>   
        </div>

    );
}
