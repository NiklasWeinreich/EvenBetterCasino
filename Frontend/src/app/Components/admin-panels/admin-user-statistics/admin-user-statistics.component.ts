import { Component, OnInit } from '@angular/core';
import { GamehistoryService } from '../../../Services/GameHistory/gamehistory.service';
import { GameHistory } from '../../../Models/gamehistory.model';
import { CommonModule } from '@angular/common';
import { UserStats } from '../../../Models/userstats.model';

@Component({
  selector: 'app-admin-user-statistics',
  imports: [CommonModule],
  templateUrl: './admin-user-statistics.component.html',
  styleUrl: './admin-user-statistics.component.css'
})
export class AdminUserStatisticsComponent implements OnInit {
  gameHistories: GameHistory[] = [];
  userStats: UserStats[] = []; // For at gemme brugerstatistikker


  constructor(private gameHistoryService: GamehistoryService) { }

  ngOnInit(): void {
    this.loadGameHistories();
  }

  loadGameHistories(): void {
    this.gameHistoryService.getGameHistory().subscribe({
      next: (histories) => {
        this.gameHistories = histories;
        this.calculateStats();
      },
      error: (err) => console.error('Fejl ved hentning af game histories:', err),
    });
  }

calculateStats(): void {
  const statsMap = new Map<number, any>();

  for (const entry of this.gameHistories) {
    if (!entry.userId || !entry.userName) continue;

    if (!statsMap.has(entry.userId)) {
      statsMap.set(entry.userId, {
        userId: entry.userId,
        userName: entry.userName,
        totalBet: 0,
        totalWin: 0,
        gamesPlayed: 0,
        wins: 0,
        cashouts: 0,
        uniqueGames: new Set<string>(),
        lastPlayed: new Date(0)
      });
    }

    const stat = statsMap.get(entry.userId);

    stat.totalBet += entry.betAmount;
    stat.totalWin += entry.winAmount;
    stat.gamesPlayed += 1;

    if (entry.isWin) stat.wins += 1;
    if (entry.wasCashedOut) stat.cashouts += 1;

    stat.uniqueGames.add(entry.gameName);

    const gameDate = new Date(entry.date);
    if (gameDate > stat.lastPlayed) {
      stat.lastPlayed = gameDate;
    }
  }

  this.userStats = Array.from(statsMap.values()).map(stat => ({
    ...stat,
    uniqueGames: Array.from(stat.uniqueGames),
    netProfit: stat.totalWin - stat.totalBet
  }));

  console.log(this.userStats); // Se den udvidede statistik
}


}