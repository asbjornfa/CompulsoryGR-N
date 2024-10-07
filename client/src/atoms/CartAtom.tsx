    import { atom } from 'jotai';


    export const cartAtom = atom<CartItem[]>([]);

    export interface CartItem {
        product_id: number;
        quantity: number;
        count: number;
    }

    
