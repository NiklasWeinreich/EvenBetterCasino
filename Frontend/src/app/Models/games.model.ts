export interface Game {
    id: number;
    name: string;
    categoryId: number;
    webUrl?: string | null;
    jackpotAmount: number;
    imageUrl: string;
    status: boolean;
    description: string;
}