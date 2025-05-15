export interface DiceGameResult {
  sessionId: string;
  message: string;
  diceValue: number;
  isWin: boolean;
  payout: number;
  winProbability: number;
}
