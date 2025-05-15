import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Game } from '../../Models/games.model';
import { GameService } from '../../Services/Games/games.service';

@Component({
  selector: 'app-games-front-page',
  imports: [CommonModule, HttpClientModule, FormsModule, RouterLink],
  templateUrl: './games-front-page.component.html',
  styleUrl: './games-front-page.component.css',
})
export class GamesComponent implements OnInit {
  games: Game[] = [];

  constructor(private gameService: GameService, private router: Router) {}

  ngOnInit(): void {
    this.loadGames();
  }

  loadGames(): void {
    this.gameService.getAllGames().subscribe((res) => {
      this.games = res;
      console.log('Spil hentet:', this.games);
    });
  }

  navigateToGame(games: Game): void {
    if (games.webUrl) {
      this.router.navigate(['/Games', games.webUrl]);
    }
  }
}
