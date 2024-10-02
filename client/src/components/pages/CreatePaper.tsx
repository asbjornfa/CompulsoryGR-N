import React from 'react';

const CreatePaperForm: React.FC = () => {
    return (
        <div className="flex justify-center items-center h-screen bg-black bg-opacity-50">
            <div className="flex gap-5">
                {/* Form Section */}
                <div className="bg-gray-300 p-8 rounded-md w-80">
                    <h2 className="text-center text-2xl font-bold mb-6">Create paper</h2>
                    <form>
                        <label htmlFor="name" className="block mb-2 text-lg">
                            Name
                        </label>
                        <input
                            type="text"
                            id="name"
                            name="name"
                            placeholder="Enter name"
                            className="w-full p-2 mb-4 border rounded-md"
                        />

                        <label htmlFor="stock" className="block mb-2 text-lg">
                            Stock
                        </label>
                        <input
                            type="text"
                            id="stock"
                            name="stock"
                            placeholder="Enter stock"
                            className="w-full p-2 mb-4 border rounded-md"
                        />

                        <label htmlFor="price" className="block mb-2 text-lg">
                            Price
                        </label>
                        <input
                            type="text"
                            id="price"
                            name="price"
                            placeholder="Enter price"
                            className="w-full p-2 mb-6 border rounded-md"
                        />

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

export default CreatePaperForm;
