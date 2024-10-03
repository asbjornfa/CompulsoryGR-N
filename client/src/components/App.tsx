import { Route, Routes } from "react-router-dom";
import React from "react";
import { Toaster } from "react-hot-toast";
import PageHeader from "./PageHeader.tsx";
import Home from "./pages/Home.tsx";
import Shop from "./pages/Shop.tsx";
import CreatePaper from "./pages/CreatePaper.tsx";

const App = () => {
    return (
        <>
            <Toaster position={"top-center"} />
            <PageHeader />
            <div className="main-content-container">
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/Shop" element={<Shop />} />
                    <Route path="/createPaper" element={<CreatePaper />} />
                </Routes>
            </div>
        </>
    );
};

export default App;
