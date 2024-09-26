import {atom} from "jotai";
import {RequestCreatePaperDTO} from "../Api.ts";

export const PaperAtom = atom<RequestCreatePaperDTO[]>([]);
export const namePaperAtom = atom<string>('');
export const stockPaperAtom = atom<number>(0);
export const pricePaperAtom = atom<number>(0);