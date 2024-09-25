import React, {useEffect} from "react";
import {useAtom} from "jotai";
import {PaperAtom} from "../atoms/PaperAtom.tsx";
import {useInitializeData} from "../useInitializeData.ts";

export default function Home() {

    const [, setPapers] = useAtom(PaperAtom);

    useEffect(() => {
        
    },[])
    
    useInitializeData();

    return (
        <div>
            <h1 className="menu-title text-5xl m-5">Hello fuckers</h1>
            <p className="font-bold">This is a template for a react project with Jotai, Typescript, DaisyUI, Vite (& more)</p>
        </div>
    );
}