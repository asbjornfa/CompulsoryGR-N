import React, {useEffect, useState} from "react";
import {useAtom} from "jotai";
import {namePaperAtom, PaperAtom, SortOrderAtom} from "../../atoms/PaperAtom.tsx";
import {Api, RequestCreatePaperDTO} from "../../Api.ts";
import PaperCard from "../Paper/PaperCard.tsx";


const api = new Api();


export default function Shop() {

    const [papers, setPapers] = useAtom(PaperAtom);
    const [sortOrder, setSortOrder] = useAtom(SortOrderAtom);
    const [searchQuery, setSearchQuery] = useState("");

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

    const filteredPapers = papers.filter(paper =>
        paper.name?.toLowerCase().includes(searchQuery.toLowerCase()) // Check if the title matches the search query
    );

    const sortedPapers = filteredPapers.sort((a, b) => {
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
        <div className="shop-container" style={{ position: 'relative' }}>
            <Sidebar setSortOrder={setSortOrder} />
            <SearchBar searchQuery={searchQuery} setSearchQuery={setSearchQuery} />
            <div className="overlay-wrapper">
                <div className="product-grid">
                    {sortedPapers.length > 0 ? (
                        sortedPapers.map((paper, index) => (
                            <PaperCard key={index} paper={paper} />
                        ))
                    ) : (
                        <p>No products found</p>
                    )}
                </div>
            </div>
        </div>
    );
}

function SearchBar({ searchQuery, setSearchQuery }) {
    return (
        <div style={{ textAlign: 'center', margin: '0' }}> {/* Ingen margin */}
            <input
                type="text"
                placeholder="Search..."
                value={searchQuery}
                onChange={(e) => setSearchQuery(e.target.value)}
                style={{
                    padding: '10px',
                    borderRadius: '4px',
                    border: '1px solid #ccc',
                    width: '300px', // Justeret bredde
                }}
            />
        </div>
    );
}





function Sidebar({ setSortOrder }) {
    return (
        <div className="sidebar" style={{ marginTop: "270px" }}>
            <h3>Filter by</h3>
            <div className="filter-options">
                <div className="filter-option">
                    <button  onClick={() => setSortOrder("priceDesc")}>Price: High to Low</button>
                    <br/>
                    <button onClick={() => setSortOrder("priceAsc")}>Price: Low to High</button>
                </div>
            </div>
        </div>
    );
}