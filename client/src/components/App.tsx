import {Route, Routes} from "react-router-dom";
import React, {useEffect} from "react";
import {Toaster} from "react-hot-toast";
import {DevTools} from "jotai-devtools";
import {useAtom} from "jotai";
import {ThemeAtom} from "../atoms/ThemeAtom.tsx";
import Home from "./pages/Home.tsx";
import Shop from "./pages/Shop.tsx";
import PageHeader from "./PageHeader.tsx";
import CreatePaper from './pages/CreatePaper';

const App = () => {

    return (<>

        <Toaster position={"top-center"}/>
        <PageHeader/>
        <Routes>
            <Route path="/Home" element={<Home/>}/>
            <Route path="/Shop" element={<Shop/>}/>
            <Route path="/createPaper" element={<CreatePaper/>} />
        </Routes>
    </>)
}

export default App;