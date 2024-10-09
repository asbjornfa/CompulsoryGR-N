import {atom} from "jotai/index";
import {ResponseCreateOrderDTO} from "../Api.ts";

export const OrderAtom = atom<ResponseCreateOrderDTO[]>([]);