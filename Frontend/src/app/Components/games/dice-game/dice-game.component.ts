import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { GameLayoutComponent } from '../game-layout/game-layout.component';
import { DiceService } from '../../../Services/Games/dice.service';
import { CommonModule } from '@angular/common';
import { DiceGameResult } from '../../../Models/dicegame.model';
import { AuthService } from '../../../Services/Security/auth.service';

@Component({
  selector: 'app-dice-game',
  standalone: true,
  imports: [FormsModule, GameLayoutComponent, CommonModule],
  templateUrl: './dice-game.component.html',
})
export class DiceGameComponent {
  selectedNumber = 50;
  isGuessOver = true;
  betAmount = 100;
  dicegameResult?: DiceGameResult;
  //dicegameHistory: DiceGameResult[] = [];

  constructor(
    private diceService: DiceService,
    private authService: AuthService
  ) {}

  playDiceGame(betAmount: number) {
    this.diceService
      .playGame(this.selectedNumber, this.isGuessOver, betAmount)
      .subscribe({
        next: (response) => {
          this.dicegameResult = response;
          console.log('Spillet kørte:', response);
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
          };
        },
      });
  }
}
