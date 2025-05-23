import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../Services/User/user.service';
import { AuthService } from '../../Services/Security/auth.service';
import { GameService } from '../../Services/Games/games.service';
import { Game } from '../../Models/games.model';
import { User } from '../../Models/user.model';

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
  welcomeMessage: string = '';
  userBalance: number = 0;


  constructor(
    private userService: UserService,
    public authService: AuthService,
    public gamesService: GameService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadUserEmail();
    this.loadGames();
  }

  navigateToGame(games: Game): void {
    if (games.webUrl) {
      this.router.navigate(['/Games', games.webUrl]);
    }
  }

  loadGames(): void {
    this.gamesService.getAllGames().subscribe({
      next: (games) => {
        this.games = games.sort(() => 0.5 - Math.random()).slice(0, 3);
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
      this.userBalance = user.balance;
      this.welcomeMessage = `Hej, ${user.firstName}!`;

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

   scrollToTop() {
  window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  ngAfterViewInit() {
    window.addEventListener('scroll', () => {
      const btn = document.getElementById('scrollToTopBtn');
      if (btn) {
        btn.style.display = window.scrollY > 200 ? 'block' : 'none';
      }
    });
  }


}
