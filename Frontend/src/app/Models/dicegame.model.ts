export interface DiceGame {
  message: string;
  diceValue: number;
  isWin: boolean;
  payout: number;
  winProbability: number;
  selectedNumber: number;
  isGuessOver: boolean;
}
