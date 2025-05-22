import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { GamehistoryService } from '../../Services/GameHistory/gamehistory.service';
import { AuthService } from '../../Services/Security/auth.service';
import { resetUser, User } from '../../Models/user.model';

@Component({
  selector: 'app-gamehistory',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './gamehistory.component.html',
  styleUrl: './gamehistory.component.css'
})
export class GamehistoryComponent {
  gameHistory: any[] = [];
  filteredGameHistory: any[] = [];
  searchTerm: string = '';


  constructor(
    private gamehistoryService: GamehistoryService,
    private authService: AuthService
  ) { }


  currentUser: User = resetUser();




  ngOnInit(): void {
    // Log user information when it's fetched
    this.authService.currentUser.subscribe((user) => {
      this.currentUser = user ?? resetUser();
      console.log('Current user fetched:', this.currentUser);
    });

    this.fetchGameHistory();

  }

  ngOnChanges() {
    this.filterGameHistory();
  }

  fetchGameHistory() {
    this.gamehistoryService.getGameHistoryByUserId(this.currentUser.id).subscribe({
      next: (data) => {
        this.gameHistory = data;
        this.filteredGameHistory = data;
      },
      error: (err) => {
        console.error('Error fetching game history:', err);
      }
    });
  }

  filterGameHistory() {
    this.filteredGameHistory = this.gameHistory.filter(ticket =>
      ticket.gameName.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
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

  getTotalBetAmount(): number {
  return this.filteredGameHistory.reduce((sum, ticket) => sum + ticket.betAmount, 0);
}

getTotalWinAmount(): number {
  return this.filteredGameHistory.reduce((sum, ticket) => sum + ticket.winAmount, 0);
}

getAverageWinAmount(): number {
  if (!this.filteredGameHistory.length) return 0;
  return this.getTotalWinAmount() / this.filteredGameHistory.length;
}


}

