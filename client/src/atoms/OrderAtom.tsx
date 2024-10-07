import {atom} from "jotai/index";
import {Order} from "../Api.ts";

export const OrderAtom = atom<Order[]>([]);