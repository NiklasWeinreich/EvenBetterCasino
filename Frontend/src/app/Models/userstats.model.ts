export interface UserStats {
  userId: number;
  userName: string;
  totalBet: number;
  totalWin: number;
  netProfit: number;
  gamesPlayed: number;
  wins: number;
  cashouts: number;
  uniqueGames: string[];
  lastPlayed: Date;
}
