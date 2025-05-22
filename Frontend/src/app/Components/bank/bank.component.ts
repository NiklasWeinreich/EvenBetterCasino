import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BalanceService } from '../../Services/Balance/Balance.service';
import { Balance } from '../../Models/Balance.model';
import { AuthService } from '../../Services/Security/auth.service';
import { User } from '../../Models/user.model';
import { TransactionService } from '../../Services/Transaction/transaction.service';

@Component({
  selector: 'app-bank',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './bank.component.html',
  styleUrls: ['./bank.component.css']
})
export class BankComponent implements OnInit {
  balance: number = 0;
  depositAmount = 0;
  public currentUser!: User
  errorMessage: string = '';
  successMessage: string = '';	  

  transactionHistory: any[] = [];
  latestTransactionId?: number;

  constructor(
    private balanceService: BalanceService,
    private authService: AuthService,
    private transactionService: TransactionService
  ) {}

  ngOnInit() {
    this.loadUser();
  }


  loadUser(): void {
    const user = this.authService.currentUserValue;

    if (user) {
      this.currentUser = user;
      this.loadBalance(user.id);
      this.getTransactionHistoryByUserId(user.id);
    } else {
      console.warn('Ingen bruger logget ind');
    }
  }

  loadBalance(userId: number) {
    this.balanceService.getBalance(userId).subscribe({
      next: (res: Balance) => {
        console.log('Saldo hentet:', res);
        this.balance = res.balance;
      },
      error: (err) => console.error('Fejl ved hentning af saldo', err)
    });
  }
  

  deposit(): void {
    if (this.depositAmount < 0) {
      this.errorMessage = 'Indtast et gyldigt beløb.';
      this.successMessage = '';
      return;
    }

    this.balanceService.depositMoney(this.currentUser.id, this.depositAmount).subscribe({
      next: () => {
        this.loadBalance(this.currentUser.id);
        this.getTransactionHistoryByUserId(this.currentUser.id);
        this.successMessage = 'Beløbet er indsat!';
        this.errorMessage = '';
        this.depositAmount = 0;
      },
      error: () => {
        this.errorMessage = 'Du skal minimum indsætte 50 kr.';
        this.successMessage = '';
      }
    });
  }

  withdraw(): void {
    if (this.depositAmount < 50) {
      this.errorMessage = 'Indtast et gyldigt beløb.';
      this.successMessage = '';
      return;
    }

    if (this.depositAmount > this.balance) {
      this.errorMessage = 'Du kan ikke hæve mere end din saldo.';
      this.successMessage = '';
      return;
    }

    this.balanceService.withdrawMoney(this.currentUser.id, this.depositAmount).subscribe({
      next: () => {
        this.loadBalance(this.currentUser.id);
        this.getTransactionHistoryByUserId(this.currentUser.id);

        this.successMessage = 'Beløbet er hævet!';
        this.errorMessage = '';
        this.depositAmount = 0;
      },
      error: () => {
        this.errorMessage = 'Fejl ved hævning.';
        this.successMessage = '';
      }
    });
  }

  getTransactionHistoryByUserId(userId: number) {
    this.transactionService.getTransactionTicketsByUserId(userId).subscribe({
      next: (data) => {
        if (data.length > 0) {
          const newest = data[0]; // Hvis nyeste er først i listen
          if (!this.transactionHistory.length || newest.transactionId !== this.transactionHistory[0]?.transactionId) {
            this.latestTransactionId = newest.transactionId;
            setTimeout(() => this.latestTransactionId = undefined, 1000);
          }
        }
        this.transactionHistory = data;
      },
      error: (err) => {
        console.error('Error fetching game history:', err);
      }
    });
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