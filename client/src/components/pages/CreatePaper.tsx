import React, {useCallback, useState} from 'react';
import { useAtom } from 'jotai';
import {Api, RequestCreatePaperDTO} from "../../Api.ts";
import {namePaperAtom, PaperAtom, pricePaperAtom, stockPaperAtom} from "../../atoms/PaperAtom.tsx";
import {PropertiesAtom} from "../../atoms/PropertiesAtom.tsx";


const CreatePaper = () => {
    const [name, setName] = useAtom(namePaperAtom);
    const [stock, setStock] = useAtom(stockPaperAtom);
    const [price, setPrice] = useAtom(pricePaperAtom);
    const [, setPapers] = useAtom(PaperAtom);
    const [props, setprops] = useAtom(PropertiesAtom);
    const [selectedProp, setSelectedProp] = useState<number>(-1);

    const api = new Api();

    // Håndtering af oprettelse af papir
    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const propserties = props.map(p => p.id!);
        const paperData: RequestCreatePaperDTO = {
            name: name,
            stock: stock,
            price: price,
            discontinued: false,
            propertyIds: propserties
        };

        try {
            const response = await api.api.paperCreatePaper(paperData);

            if (response.status === 200 || response.status === 201) {
                console.log("Paper created successfully!");
            } else {
                console.log("Error: " + response.status + " " + response.statusText);
            }
        } catch (error) {
            console.error("Error details: ", error);  // For a more detailed error log
        }
    };

    return (
        <div className="flex justify-center items-center h-screen bg-black bg-opacity-50">
            <div className="flex gap-5">
                {/* Form Section */}
                <div className="bg-gray-300 p-8 rounded-md w-80">
                    <h2 className="text-center text-2xl font-bold mb-6">Create paper</h2>
                    <form onSubmit={handleSubmit}>
                        <label htmlFor="name" className="block mb-2 text-lg">
                            Name
                        </label>
                        <input
                            type="text"
                            id="name"
                            name="name"
                            placeholder="Enter name"
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                            className="w-full p-2 mb-4 border rounded-md"
                        />

                        <label htmlFor="stock" className="block mb-2 text-lg">
                            Stock
                        </label>
                        <input
                            type="number"
                            id="stock"
                            name="stock"
                            placeholder="Enter stock"
                            value={stock}
                            onChange={(e) => setStock(Number(e.target.value))}
                            className="w-full p-2 mb-4 border rounded-md"
                        />

                        <label htmlFor="price" className="block mb-2 text-lg">
                            Price
                        </label>
                        <input
                            type="number"
                            id="price"
                            name="price"
                            placeholder="Enter price"
                            value={price}
                            onChange={(e) => setPrice(Number(e.target.value))}
                            className="w-full p-2 mb-6 border rounded-md"
                        />

                        <label htmlFor="properties" className="block mb-2 text-lg">
                            Properties
                        </label>

                        <select className="w-full p-2 mb-6 border rounded-md" onChange={function (e) {
                            console.log(e)
                            return setSelectedProp(Number(e.target.value));
                        }} >

                            {props.map(p => (<option value={p.id}>{p.propertyName}</option>))}

                        </select>
                        <button
                            type="submit"
                            className="w-full p-2 bg-green-700 text-white rounded-md hover:bg-green-600"
                        >
                            Create
                        </button>
                    </form>
                </div>

                {/* Placeholder for another section */}
                <div className="bg-gray-300 w-80 h-96 rounded-md"></div>
            </div>
        </div>
    );
};

export default CreatePaper;
