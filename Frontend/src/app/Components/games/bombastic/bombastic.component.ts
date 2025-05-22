import { Component, EventEmitter, Output } from '@angular/core';
import { GameLayoutComponent } from '../game-layout/game-layout.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BombasticService } from '../../../Services/Games/bombasticgame.service';
import { bombasticGame } from '../../../Models/bombastic.model';
import { BalanceService } from '../../../Services/Balance/Balance.service';
import { error } from 'console';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-bombastic',
  imports: [GameLayoutComponent, FormsModule, CommonModule],
  templateUrl: './bombastic.component.html',
  styleUrl: './bombastic.component.css',
  standalone: true,
})
export class BombasticComponent {


  @Output() cashOutAmount = new EventEmitter<number>();
  isOngoing = false;
  betAmount = 50;
  sessionId: string | null = null;
  gameStarted = false;
  bombsClicked = 0;
  lastPayout = 0;
  multiplier = 1;
  message = '';

  constructor(private bombasticService: BombasticService,
              private balanceService: BalanceService
  ) {}

startGame(bet: number) {
  this.betAmount = bet;
  this.bombasticService.startGame(bet).subscribe({
    next: (res) => {
      this.sessionId = res.sessionId;
      this.gameStarted = true;
    },
    error: (error) => {
  console.error('❌ Fejl ved start af spil:', error);
  if (error instanceof HttpErrorResponse) {
    console.error('Status:', error.status);
    console.error('Fejlmeddelelse:', error.message);
    console.error('Fejl-body:', error.error);
  }
}
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
        this.cashOutAmount.emit(this.lastPayout); // 💥 send op til game-layout


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

  // cashOut() {
  //   if (!this.sessionId) return;

  //   this.balanceService.winGame(this.sessionId).subscribe({
  //     next: (res: bombasticGame) => {
  //       this.message = res.message;
  //       this.bombsClicked = res.currentClickNumber;
  //       this.multiplier = res.currentMulitplier;
  //       this.lastPayout = res.currentWinAmount;
  //       this.gameStarted = false;
  //     },
  //     error: () => {
  //       this.message = 'Der opstod en fejl ved indløsning.';
  //     },
  //   });
  // }
}
