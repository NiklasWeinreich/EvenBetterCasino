import { Component, OnInit } from '@angular/core';
import { GameService } from '../../../Services/Games/games.service';
import { Game } from '../../../Models/games.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-games',
  imports: [CommonModule],
  templateUrl: './admin-games.component.html',
  styleUrl: './admin-games.component.css'
})
export class AdminGamesComponent implements OnInit {
  games: Game[] = [];

  constructor(private gameService: GameService) {}

  ngOnInit(): void {
    this.loadGames();
  }

  loadGames(): void {
    this.gameService.getAllGames().subscribe({
      next: (games => {
        this.games = games;
        console.log('Spil hentet', games);
      }),
      error: (err) => {
        console.error(err);
        console.log('Fejl ved hentning af spil' + err);
      },
    })
  }
}
