import { Component } from '@angular/core';
import { GameLayoutComponent } from '../game-layout/game-layout.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BombasticService } from '../../../Services/Games/bombasticgame.service';
import { bombasticGame } from '../../../Models/bombastic.model';

@Component({
  selector: 'app-bombastic',
  imports: [GameLayoutComponent, FormsModule, CommonModule],
  templateUrl: './bombastic.component.html',
  styleUrl: './bombastic.component.css',
  standalone: true,
})
export class BombasticComponent {
  jackpot = 500000;
  betAmount = 50;
  sessionId: string | null = null;
  gameStarted = false;
  bombsClicked = 0;
  lastPayout = 0;
  multiplier = 1;
  message = '';

  constructor(private bombasticService: BombasticService) {}

  startGame(bet: number) {
    this.betAmount = bet;
    this.bombasticService.startGame(bet).subscribe({
      next: (res) => {
        this.sessionId = res.sessionId;
        this.message = res.message;
        this.gameStarted = true;
        this.bombsClicked = 0;
        this.lastPayout = 0;
        this.multiplier = 1;
      },
      error: () => {
        this.message = 'Noget gik galt ved start.';
      },
    });
  }

  clickBomb() {
    if (!this.sessionId) return;

    this.bombasticService.clickBomb(this.sessionId).subscribe({
      next: (res: bombasticGame) => {
        this.message = res.message;
        this.bombsClicked = res.currentClickNumber;
        this.multiplier = res.currentMulitplier;
        this.lastPayout = res.currentWinAmount;

        if (res.isExploded) {
          this.message = '💥 Bomben eksploderede!';
          this.gameStarted = false;
        }
      },
      error: () => {
        this.message = 'Der opstod en fejl ved klik.';
      },
    });
  }
}
