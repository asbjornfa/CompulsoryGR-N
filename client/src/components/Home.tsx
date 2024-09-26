import React, {useEffect} from "react";
import {useAtom} from "jotai";
import {namePaperAtom, stockPaperAtom, pricePaperAtom} from "../atoms/PaperAtom.tsx";
import {useInitializeData} from "../useInitializeData.ts";

export default function Home() {

    const [paperName, setPaperName] = useAtom(namePaperAtom);
    const [paperStock, setPaperStock] = useAtom(stockPaperAtom);
    const [paperPrice, setPaperPrice] = useAtom(pricePaperAtom);

    useEffect(() => {

    }, [])

    useInitializeData();

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
        } catch (error) {
            console.error("Error!", error);
        }
    };

    return (
        <>
            <div>
                <h1 className="menu-title text-5xl m-5">Hello fuckers</h1>
                <p className="font-bold">This is a template for a react project with Jotai, Typescript, DaisyUI, Vite (&
                    more)</p>
                <h2>Create paper</h2>

            </div>
            <div>
                <input
                    type="text"
                    placeholder="name"
                    value={paperName}
                    onChange={(e) => setPaperName(e.target.value)}
                />
                <input
                    type={"number"}
                    placeholder="Stock"
                    value={paperStock}
                    onChange={(e) => {setPaperStock(parseInt(e.target.value))}}
                />
                <input
                    type="number"
                    placeholder="price"
                    value={paperPrice}
                    onChange={(e) => {setPaperPrice(parseFloat(e.target.value))}}
                />
                <button className="btn" type="button" onClick={handleCreatePaper}> Create paper</button>
            </div>
        </>

    );
}