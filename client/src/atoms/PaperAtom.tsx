import {atom} from "jotai";
import {RequestCreatePaperDTO} from "../Api.ts";

export const PaperAtom = atom<RequestCreatePaperDTO[]>([]);
export const namePaperAtom = atom<string>('');
export const stockPaperAtom = atom<number>(0);
export const pricePaperAtom = atom<number>(0);

export const CreateNewPaperAtom = atom<RequestCreatePaperDTO>({
    name: '',
    stock: 0,
    price: 0,
    discontinued: false
})

export const searchAtom = atom<string>(""); // For search text
export const minPriceAtom = atom<number | undefined>(undefined); // For minimum price
export const maxPriceAtom = atom<number | undefined>(undefined); // For maximum price
export const sortByAtom = atom<string>(""); // For sorting field (name, price, stock)
export const sortOrderAtom = atom<string>("asc"); // For sort order (ascending or descending)