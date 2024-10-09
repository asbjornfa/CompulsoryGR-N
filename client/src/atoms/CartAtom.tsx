import { atom } from 'jotai';
import {RequestCreateOrderDTO} from '../Api.ts'

    export const cartAtom = atom<RequestCreateOrderDTO>({
        
    });

    export interface CartItem {
        product_id: number;
        quantity: number;
        count: number;
    }

    
