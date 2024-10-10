import {atom} from "jotai";
import {RequestCreatePaperDTO, ResponseCreatePaperDTO} from "../Api.ts";

export const PaperAtom = atom<ResponseCreatePaperDTO[]>([]);
export const namePaperAtom = atom<string>('');
export const stockPaperAtom = atom<number>(0);
export const pricePaperAtom = atom<number>(0);
export const SortOrderAtom = atom<'priceAsc' | 'priceDesc' | 'nameAsc' | 'nameDesc'>('priceDesc');

export const CreateNewPaperAtom = atom<RequestCreatePaperDTO>({
    name: '',
    stock: 0,
    price: 0,
    discontinued: false
})