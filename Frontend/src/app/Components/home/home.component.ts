import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../Services/User/user.service';
import { AuthService } from '../../Services/Security/auth.service';
import { GameService } from '../../Services/Games/games.service';
import { Game } from '../../Models/games.model';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  userEmail: string = '';
  isSubscribed: boolean = false;
  message: string = '';
  messageClass: string = '';
  games: Game[] = [];

  constructor(
    private userService: UserService,
    public authService: AuthService,
    public gamesService: GameService
  ) {}

  ngOnInit(): void {
    this.loadUserEmail();
    this.loadGames();
  }

  loadGames(): void {
    this.gamesService.getAllGames().subscribe({
      next: (games) => {
        this.games = games;
        console.log('spil hentet', games);

      },
      error: (err) => {
        console.error(err);
        console.log('Fejl ved hentning af spil' + err);
      },
    });
  }

  loadUserEmail(): void {
    const user = this.authService.currentUserValue;
    if (user && user.email) {
      this.userEmail = user.email;
      this.isSubscribed = user.newsLetterIsSubscribed;
    }

  }

  subscribeToNewsletter() {
    if (!this.userEmail) return;

    this.userService.subscribe(this.userEmail).subscribe({
      next: () => {
        this.isSubscribed = true;
        this.message = 'Du er nu tilmeldt nyhedsbrevet!';
        this.messageClass = 'text-success';
      },
      error: (err) => {
        console.error(err);
        this.message = 'Noget gik galt under tilmelding.';
        this.messageClass = 'text-danger';
      },
    });
  }

  unsubscribeFromNewsletter() {
    if (!this.userEmail) return;

    this.userService.unsubscribe(this.userEmail).subscribe({
      next: () => {
        this.isSubscribed = false;
        this.message = 'Du er nu afmeldt nyhedsbrevet.';
        this.messageClass = 'text-warning';
      },
      error: (err) => {
        console.error(err);
        this.message = 'Noget gik galt under afmelding.';
        this.messageClass = 'text-danger';
      },
    });
  }

  alreadySubscribed(): boolean {
  return this.isSubscribed;
}

}
