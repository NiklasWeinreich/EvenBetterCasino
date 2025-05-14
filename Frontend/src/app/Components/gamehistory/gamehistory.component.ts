import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { GamehistoryService } from '../../Services/GameHistory/gamehistory.service';

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

  constructor(private gamehistoryService: GamehistoryService) {}

  ngOnInit() {
    this.fetchGameHistory();
  }

  fetchGameHistory() {
    this.gamehistoryService.getGameHistory().subscribe({
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
