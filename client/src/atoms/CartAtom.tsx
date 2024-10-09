import { atom } from 'jotai';
import { RequestCreateOrderDTO } from '../Api.ts';

export const cartAtom = atom<{ dtos: CartItem[] }>({
    dtos: [] // Initialiserer som en tom array
});

// Interface for CartItem
export interface CartItem {
    product_id: number;
    quantity: number;
}
