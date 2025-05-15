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
  ) {}


    currentUser: User = resetUser();




    ngOnInit(): void {
    // Log user information when it's fetched
    this.authService.currentUser.subscribe((user) => {
      this.currentUser = user ?? resetUser();
      console.log('Current user fetched:', this.currentUser);
    });

        this.fetchGameHistory();

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

  ngOnChanges() {
    this.filterGameHistory();
  }

  filterGameHistory() {
    this.filteredGameHistory = this.gameHistory.filter(ticket =>
      ticket.gameName.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }
}
