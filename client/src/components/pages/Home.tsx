import React, { useEffect } from "react";
import { useAtom } from "jotai";
import { namePaperAtom, stockPaperAtom, pricePaperAtom, PaperAtom } from "../../atoms/PaperAtom.tsx";

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
            <main className="main-content">
                <div className="hero-section">
                    <h1>Welcome to JA Paper</h1>
                    <p>Welcome to JA Paper – Your reliable partner for business paper supplies. Easily browse, customize, and manage your orders to meet your company's needs.</p>
                    <button className="shop-now-button">Shop Now</button>
                    <div className="dots">
                        <span className="dot active"></span>
                        <span className="dot"></span>
                        <span className="dot"></span>
                    </div>
                </div>
            </main>
    );
};
