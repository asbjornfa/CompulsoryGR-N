import React, { useEffect } from "react";
import { useAtom } from "jotai";
import { namePaperAtom, stockPaperAtom, pricePaperAtom, PaperAtom } from "../../atoms/PaperAtom";
import { useNavigate } from "react-router-dom";

export default function Home() {
    const [paperName, setPaperName] = useAtom(namePaperAtom);
    const [paperStock, setPaperStock] = useAtom(stockPaperAtom);
    const [paperPrice, setPaperPrice] = useAtom(pricePaperAtom);
    const [papers, setPapers] = useAtom(PaperAtom);

    const navigate = useNavigate();

    useEffect(() => {
        const fetchPapers = async () => {
            try {
                const response = await fetch('http://localhost:5000/api/paper');
                if (!response.ok) throw new Error('Failed to fetch papers');
                const data = await response.json();
                setPapers(data);
            } catch (error) {
                console.error("Error fetching papers:", error);
            }
        };

        fetchPapers();
    }, [setPapers]);

    const handleCreatePaper = async () => {
        const createPaperDTO = { name: paperName, stock: paperStock, price: paperPrice };

        try {
            const response = await fetch('http://localhost:5000/api/paper', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(createPaperDTO),
            });

            if (response.ok) {
                const newPaper = await response.json();
                setPapers((prev) => [...prev, newPaper]);
            }
        } catch (error) {
            console.error("Error creating paper:", error);
        }
    };

    const handleShopNowClick = () => {
        navigate("/Shop");
    };

    return (
        <main className="main-content">
            <div
                className="absolute inset-0 flex flex-col justify-center items-center text-center text-white">
                {/* Overlay */}
                <div className="absolute inset-0 bg-black bg-opacity-50"></div>

                {/* Hero Content */}
                <div className="relative text-center text-white z-10">
                    
                    <h1 className="text-5xl font-bold mb-4">Welcome to JA Paper</h1>
                    <p className="text-lg mb-8">Your reliable partner for business paper supplies. 
                        Easily browse, customize, and manage your orders to meet your company's needs.</p>
                    <button className="shop-now-button bg-green-700 hover:bg-green-600 text-white py-2 px-6 rounded"
                            onClick={handleShopNowClick}>Shop Now
                    </button>

                    {/* Dots Navigation */}
                    <div className="dots mt-8 flex justify-center space-x-2">
                        <span className="dot w-3 h-3 bg-white rounded-full opacity-70"></span>
                        <span className="dot w-3 h-3 bg-white rounded-full opacity-50"></span>
                        <span className="dot w-3 h-3 bg-white rounded-full opacity-50"></span>
                    </div>
                </div>
            </div>
        </main>
    );
}
