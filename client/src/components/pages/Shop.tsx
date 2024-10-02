import React, {useEffect} from "react";
import {useAtom} from "jotai";
import {namePaperAtom, PaperAtom} from "../../atoms/PaperAtom.tsx";
import {Api, RequestCreatePaperDTO} from "../../Api.ts";
import {Simulate} from "react-dom/test-utils";
import error = Simulate.error;
import PaperCard from "../Paper/PaperCard.tsx";
import {searchAtom, minPriceAtom, maxPriceAtom, sortByAtom, sortOrderAtom} from "../../atoms/PaperAtom.tsx";


const api = new Api();


export default function Shop() {

    const [papers, setPapers] = useAtom(PaperAtom);
    const [search, setSearch] = useAtom(searchAtom);
    const [minPrice, setMinPrice] = useAtom(minPriceAtom);
    const [maxPrice, setMaxPrice] = useAtom(maxPriceAtom);
    const [sortBy, setSortBy] = useAtom(sortByAtom);
    const [sortOrder, setSortOrder] = useAtom(sortOrderAtom);

    useEffect(() => {
        const fetchPapers = async () => {
            try {
                const response = await api.api.paperGetPapers({
                    search,
                    minPrice,
                    
                }); // Fetch papers using the API
                const data: RequestCreatePaperDTO[] = await response.data as unknown as RequestCreatePaperDTO[]; // Extract the data array
                setPapers(data); // Set the papers in the atom
            } catch (error) {
                console.error("Failed to fetch papers:", error); // Handle fetch errors
            }
        };

        fetchPapers(); // Call the fetch function on mount
    }, [setPapers]);

    return (
        <div className="shop-container">
            <div className="product-grid">
                {papers.map((paper, index) => (
                    <PaperCard key={index} paper={paper} />
                ))}
            </div>
        </div>
    );
}

/*const Sidebar = () => (
    <div className="sidebar">
        <h3>Filter by</h3>
        <div className="filter-options">
            {/* Add your filters here *//*
            <div className="filter-option">
                <label htmlFor="price-range">Price</label>
                <input type="range" id="price-range" min="0" max="500" />
            </div>
        </div>
    </div>
);
*/

