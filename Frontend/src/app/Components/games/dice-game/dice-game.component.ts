import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { GameLayoutComponent } from '../game-layout/game-layout.component';
import { DiceService } from '../../../Services/Games/dice.service';
import { CommonModule } from '@angular/common';
import { DiceGame } from '../../../Models/dicegame.model';
import { AuthService } from '../../../Services/Security/auth.service';

@Component({
  selector: 'app-dice-game',
  standalone: true,
  imports: [FormsModule, GameLayoutComponent, CommonModule],
  templateUrl: './dice-game.component.html',
  styleUrls: ['./dice-game.component.css'],
})
export class DiceGameComponent {
  selectedNumber = 50;
  isGuessOver = true;
  betAmount = 50;
  dicegameResult?: DiceGame;
  payout = 0;

  /** Animation flags **/
  showDiceAnimation = false;
  diceAnimationKey = 0;

  constructor(
    private diceService: DiceService,
    private authService: AuthService
  ) {}

  playDiceGame(betAmount: number) {
    this.diceAnimationKey++;
    this.showDiceAnimation = true;

    setTimeout(() => {
      this.showDiceAnimation = false;
      this.runDiceService(betAmount);
    }, 1500);
  }

  private runDiceService(bet: number) {
    this.betAmount = bet;
    this.diceService
      .playGame(this.selectedNumber, this.isGuessOver, bet)
      .subscribe({
        next: (response: DiceGame) => {
          this.dicegameResult = {
            ...response,
            selectedNumber: this.selectedNumber,
            isGuessOver: this.isGuessOver,
          };
        },
        error: (error) => {
          console.error('Fejl:', error);
          this.dicegameResult = {
            sessionId: '',
            message: 'Der opstod en fejl under spillet.',
            diceValue: 0,
            isWin: false,
            payout: 0,
            winProbability: 0,
            selectedNumber: this.selectedNumber,
            isGuessOver: this.isGuessOver,
          };
        },
      });
  }
}
