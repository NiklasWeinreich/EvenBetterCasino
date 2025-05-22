export interface GameHistory {
    id: number;
    date: string;
    dateFormatted: string;
    userId: number;
    userName?: string;
    gameId: number;
    gameName?: string;
    betAmount: number;
    winAmount: number;
    isWin: boolean;
    wasCashedOut?: boolean;
}
