export interface DiceGame {
  sessionId: string;
  message: string;
  diceValue: number;
  isWin: boolean;
  payout: number;
  winProbability: number;
  selectedNumber: number;
  isGuessOver: boolean;
}
