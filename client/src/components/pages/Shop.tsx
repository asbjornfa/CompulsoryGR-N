import React, {useEffect} from "react";
import {useAtom} from "jotai";
import {namePaperAtom, PaperAtom, SortOrderAtom} from "../../atoms/PaperAtom.tsx";
import {Api, RequestCreatePaperDTO} from "../../Api.ts";
import {Simulate} from "react-dom/test-utils";
import error = Simulate.error;
import PaperCard from "../Paper/PaperCard.tsx";


const api = new Api();


export default function Shop() {

    const [papers, setPapers] = useAtom(PaperAtom);
    const [sortOrder, setSortOrder] = useAtom(SortOrderAtom);

    useEffect(() => {
        const fetchPapers = async () => {
            try {
                const response = await api.api.paperGetPapers(); // Fetch papers using the API
                const data: RequestCreatePaperDTO[] = await response.data as unknown as RequestCreatePaperDTO[]; // Extract the data array
                setPapers(data); // Set the papers in the atom
            } catch (error) {
                console.error("Failed to fetch papers:", error); // Handle fetch errors
            }
        };

        fetchPapers(); // Call the fetch function on mount
    }, [setPapers]);

    const sortedPapers = papers.sort((a, b) => {
        switch (sortOrder) {
            case 'priceAsc':
                return (a.price || 0) - (b.price || 0);
            case 'priceDesc':
                return (b.price || 0) - (a.price || 0);
            default:
                return 0;
        }
    });
    return (
        <div className="shop-container">
            <Sidebar setSortOrder={setSortOrder}  />
            <div className="overlay-wrapper">
            <div className="product-grid">
                {sortedPapers.map((paper, index) => (
                    <PaperCard key={index} paper={paper} />
                ))}
            </div>
        </div>
    </div>
    );
}


function Sidebar({ setSortOrder }) {
    return (
        <div className="sidebar" style={{ marginTop: "270px"}}> {/* Add margin-top here */}
            <h3>Filter by</h3>
            <div className="filter-options">
                <div className="filter-option">
                    <label htmlFor="price-range">Price</label>
                    <button onClick={() => setSortOrder("priceDesc")}>Price: High to Low</button>
                    <button onClick={() => setSortOrder("priceAsc")}>Price: Low to High</button>
                </div>
            </div>
        </div>
    );
}