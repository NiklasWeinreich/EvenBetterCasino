import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { GameLayoutComponent } from '../game-layout/game-layout.component';
import { KenoService } from '../../../Services/Games/keno.service';
import { KenoGetOdds } from '../../../Models/Keno/keno.getodds';
import { kenoPlayGame } from '../../../Models/Keno/keno.playgame';
import { AuthService } from '../../../Services/Security/auth.service';

@Component({
  selector: 'app-keno',
  standalone: true,
  imports: [CommonModule, FormsModule, GameLayoutComponent],
  templateUrl: './keno.component.html',
  styleUrl: './keno.component.css',
})
export class KenoComponent implements OnInit {
  userId: number | null = null;
  selectedNumbers: number[] = [];
  betAmount: number = 10;
  gameId: number = 1;
  maxNumbers: number = 10;

  drawnNumbers: number[] = [];
  matches: number = 0;
  multiplier: number = 0;
  payout: number = 0;
  isWin: boolean = false;

  numberPool: number[] = Array.from({ length: 40 }, (_, i) => i + 1);

  constructor(
    private kenoService: KenoService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    const user = this.authService.currentUserValue;
    if (user) {
      this.userId = user.id;
    } else {
      console.warn('Bruger er ikke logget ind.');
    }
  }

  playKeno(betAmount: number): void {
    if (!this.userId) {
      console.warn('Ingen bruger-ID tilgængelig.');
      return;
    }

    const request: KenoGetOdds = {
      userId: this.userId,
      gameId: this.gameId,
      betAmount: betAmount,
      playerNumbers: this.selectedNumbers,
    };

    this.kenoService.playGame(request).subscribe({
      next: (result: kenoPlayGame) => {
        this.drawnNumbers = result.drawnNumbers;
        this.matches = result.matches;
        this.multiplier = result.multiplier;
        this.payout = result.payout;
        this.isWin = result.isWin;
      },
      error: (err) => {
        console.error('Fejl ved Keno-spil:', err);
      },
    });
  }

  toggleNumber(num: number): void {
    if (this.selectedNumbers.includes(num)) {
      this.selectedNumbers = this.selectedNumbers.filter((n) => n !== num);
    } else if (this.selectedNumbers.length < this.maxNumbers) {
      this.selectedNumbers.push(num);
    }
  }

  isMatch(num: number): boolean {
    return this.drawnNumbers.includes(num);
  }

  getMissedNumbers(): number[] {
    return this.selectedNumbers.filter((n) => !this.isMatch(n));
  }

  resetGame(): void {
    this.drawnNumbers = [];
    this.matches = 0;
    this.multiplier = 0;
    this.payout = 0;
    this.isWin = false;
  }

  resetAll(): void {
    // this.selectedNumbers = [];
    this.resetGame();
  }
}
