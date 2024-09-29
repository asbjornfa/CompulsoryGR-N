import {BrowserRouter as Router, Route, Routes} from "react-router-dom";
import Home from "../pages/Home.tsx";
import Shop from "../pages/Shop.tsx";
import AboutUs from "../pages/AboutUs.tsx";


const appRoutes = () => (
    <Routes>
        <Route path="/" element={<Home/>}/>
        <Route path="/shop" element={<Shop/>}/>

    </Routes>
)

export default appRoutes;