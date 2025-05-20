import { Component } from '@angular/core';
import { environment } from '../../../Environments/environment';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { GameLayoutComponent } from '../game-layout/game-layout.component';
import { YatzyService } from '../../../Services/Games/yatzy.service';
import { Game } from '../../../Models/games.model';
import { Observable, finalize } from 'rxjs';
import { YatzyGame } from '../../../Models/Yatzy.model';

@Component({
  selector: 'app-yatzy-game',
  standalone: true,
  imports: [FormsModule, CommonModule, GameLayoutComponent],
  templateUrl: './yatzy-game.component.html',
  styleUrl: './yatzy-game.component.css',
})
export class YatzyGameComponent {
  betAmount = 50;
  diceResults: number[] = [];
  winDiceIndexes: number[] = [];

  combinationText = '';
  payout = 0;
  isLoading = false;
  showWinAlert = false;


  constructor(private yatzyService: YatzyService) {}

  playYatzyGame(betAmount: number) {
    this.betAmount = betAmount;
    this.isLoading = true;
    this.showWinAlert = false; // reset før nyt spil

    this.yatzyService
      .playGame(betAmount)
      .pipe(finalize(() => (this.isLoading = false)))
      .subscribe({
        next: (res: YatzyGame) => {
          this.diceResults = res.diceRolls;
          this.combinationText = res.combination;
          this.payout = res.payout;

          if (this.payout > 0) {
          this.showWinAlert = true;
          setTimeout(() => {
            this.showWinAlert = false;
          }, 1000); // forsvinder efter 4 sek.
        }
        },
        error: (err) => {
          console.error('Spilfejl:', err);
        },
      });
  }

  trackByIndex(index: number): number {
    return index;
  }

}
