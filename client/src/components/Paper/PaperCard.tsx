import {RequestCreatePaperDTO} from "../../Api.ts";
import React from "react";

interface ProductCardProps {
    paper: RequestCreatePaperDTO;
}

function PaperCard({ paper }: ProductCardProps) {

    return (
        <div className="product-card">
            <img src="path-to-image" alt={paper.name} className="product-image"/>
            <h2>{paper.name}</h2>
            <p>{paper.stock ? "In Stock" : "Out of Stock"}</p>
            <div className="quantity-control">
                <button>-</button>
                <span>1</span>
                <button>+</button>
            </div>
            <button className="add-to-cart">Add to Cart</button>
        </div>
    )
}

export default PaperCard;