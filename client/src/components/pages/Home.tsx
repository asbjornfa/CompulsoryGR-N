
import { useNavigate } from "react-router-dom";

export default function Home() {

    const navigate = useNavigate();

    const handleShopNowClick = () => {
        navigate("/Shop");
    };

    return (
        <main className="main-content">
            <div
                className="absolute inset-0 flex flex-col justify-center items-center text-center text-white">

                <div className="absolute inset-0 bg-black bg-opacity-50"></div>


                <div className="relative text-center text-white z-10">
                    
                    <h1 className="text-5xl font-bold mb-4">Welcome to JA Paper</h1>
                    <p className="text-lg mb-8">Your reliable partner for business paper supplies. 
                        Easily browse, customize, and manage your orders to meet your company's needs.</p>
                    <button className="shop-now-button bg-green-700 hover:bg-green-600 text-white py-2 px-6 rounded"
                            onClick={handleShopNowClick}>Shop Now
                    </button>


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
