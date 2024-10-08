import {atom} from "jotai/index";
import {Order, RequestCreateOrderDTO} from "../Api.ts";

export const OrderAtom = atom<Order[]>([]);