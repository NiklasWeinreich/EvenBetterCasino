import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Game } from '../../Models/games.model';
import { GameService } from '../../Services/Games/games.service';
import { AuthService } from '../../Services/Security/auth.service';

@Component({
  selector: 'app-games-front-page',
  imports: [CommonModule, HttpClientModule, FormsModule],
  templateUrl: './games-front-page.component.html',
  styleUrl: './games-front-page.component.css',
})
export class GamesComponent implements OnInit {
  games: Game[] = [];
  loading: boolean = false;
  isLoggedIn: boolean = false;

  constructor(private gameService: GameService, private router: Router, private authService: AuthService) {}

  ngOnInit(): void {
    this.isLoggedIn = this.authService.currentUserValue != null && this.authService.currentUserValue.id > 0;
    this.loadGames();
  }

  

  loadGames(): void {
    this.loading = true;
    this.gameService.getAllGames().subscribe((res) => {
      this.games = res;
      this.loading = false;
      console.log('Spil hentet:', this.games);
    });
  }

   navigateToGame(game: Game): void {
    if (!this.isLoggedIn) {
      alert('Du skal være logget ind for at spille!');
      return;
    }
    if (game.webUrl) {
      this.router.navigate(['/games', game.webUrl]);
    }
  }
}