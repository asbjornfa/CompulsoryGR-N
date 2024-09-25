import {useAtom} from "jotai";
import {PaperAtom} from "./atoms/PaperAtom.tsx";
import {useEffect} from "react";
import {http} from "./http.ts";


export function useInitializeData() {
    
    const [, setPaper] = useAtom(PaperAtom);


    useEffect(() => {
        http.api.paperGetPapers()
            .then((response) => {
                console.log(response); // Log hele responsen for debugging
                // Antag, at responsen fra API'et er i det format, vi forventer
                if (response && Array.isArray(response.data)) {
                    setPaper(response.data);
                } else {
                    console.error("Response data is not an array", response);
                }
            })
            .catch((e) => {
                console.error("Error fetching papers:", e);
            });
    }, []);
}