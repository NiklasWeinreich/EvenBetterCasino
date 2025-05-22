import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../../Services/Security/auth.service';
import { User } from '../../../Models/user.model';
import { BalanceService } from '../../../Services/Balance/Balance.service';
import { GamehistoryService } from '../../../Services/GameHistory/gamehistory.service';
import { GameService } from '../../../Services/Games/games.service';
import { error } from 'console';


@Component({
  selector: 'app-game-layout',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule],
  templateUrl: './game-layout.component.html',
  styleUrls: ['./game-layout.component.css'],
})
export class GameLayoutComponent {
  @Input() betAmount!: number;
  @Input() gameName!: string;
  @Input() canCashOut!: boolean;
  @Input() gameStarted : boolean = false;

  @Output() betPlaced = new EventEmitter<number>();
  @Output() cashOutPlaced = new EventEmitter<number>();
  
  gameId: number | null = null;
  gamehistoryList: any[] = [];
  usersGamehistoryListSliced: any[] = [];
  usersGamehistoryList: any[] = [];
  balance: number = 0;
  currentUser?: User | null;


  constructor(private authService: AuthService,
    private balanceService: BalanceService,
    private gamehistoryService: GamehistoryService,
    private gameService: GameService,
  ) { }


  ngOnInit() {
  this.authService.currentUser.subscribe(user => {
    this.currentUser = user;
    this.balance = this.currentUser?.balance ?? 0;

    if (this.currentUser && this.currentUser.id != null) {
      // Hent saldo fra server for at få den opdaterede værdi
      this.balanceService.getBalance(this.currentUser.id).subscribe({
        next: (data) => {
          this.currentUser!.balance = data.balance;
          this.balance = data.balance;
        },
        error: (err) => {
          console.error('Error fetching balance:', err);
        }
      });
    }
  });

  // Hent spil og sæt gameId
  this.gameService.getAllGames().subscribe({
    next: (games) => {
      const matchedGame = games.find(game => game.name.toLowerCase() === this.gameName.toLowerCase());
      if (matchedGame) {
        this.gameId = matchedGame.id;
        this.fetchUsersGameHistory();
        this.fetchGameHistory();
      } else {
        console.warn(`Kunne ikke finde spil med navnet: ${this.gameName}`);
      }
    },
    error: (err) => {
      console.error('Fejl ved hentning af spil:', err);
    }
  });
}


  placeBet() {
    if (this.betAmount < 1) {
      alert('Indsats skal være mindst 1 kr');
      return;
    }
    this.betPlaced.emit(this.betAmount);

    if (this.currentUser && this.currentUser.id != null) {
      this.balanceService.getBalance(this.currentUser.id).subscribe({
        next: (data) => {
          if (this.currentUser) {
            this.currentUser.balance = data.balance;
            this.fetchUsersGameHistory()
            this.fetchGameHistory();

          }
          console.log('Balance:', data);
        },
        error: (err) => {
          console.error('Error fetching balance:', err);
        }
      });

    }
    else {
      console.error('No current user or user ID found.');
    }

  }

 
  fetchGameHistory() {
    if (this.gameId !== null) {
      this.gamehistoryService.getGameHistoryByGameId(this.gameId).subscribe({
        next: (data) => {
          this.gamehistoryList = data;

        },
        error: (err) => {
          console.error('Error fetching game history:', err);
        }
      });
    } else {
      console.error('Game ID is null. Cannot fetch game history.');
    }

  }


  fetchUsersGameHistory() {
    if (this.gameId !== null && this.currentUser && this.currentUser.id != null) {
      this.gamehistoryService.getGameHistoryByUserIdAndGameId(this.currentUser.id, this.gameId).subscribe({
        next: (data) => {
          this.usersGamehistoryList = data;
          this.usersGamehistoryListSliced = data.slice(0, 5);


        },
        error: (err) => {
          console.error('Error fetching game history:', err);
        }
      });
    } else {
      console.error('Game ID is null. Cannot fetch game history.');
    }

  }

  
 

  get userTotalBetAmount(): number {
    return this.usersGamehistoryList.reduce((total, ticket) => total + ticket.betAmount, 0);
  }

  get userTotalWinAmount(): number {
    return this.usersGamehistoryList.reduce((total, ticket) => total + ticket.winAmount, 0);
  }

  get userProfit(): number {
    return this.userTotalWinAmount - this.userTotalBetAmount;
  }

  get gameTotalBetAmount(): number {
    return this.gamehistoryList.reduce((total, ticket) => total + ticket.betAmount, 0);
  }

  get gameTotalWinAmount(): number {
    return this.gamehistoryList.reduce((total, ticket) => total + ticket.winAmount, 0);
  }

  get gameProfit(): number {
    return this.gameTotalWinAmount - this.gameTotalBetAmount;
  }
}
