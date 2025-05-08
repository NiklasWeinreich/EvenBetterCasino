import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { User} from '../../Models/user.model';
import { UserService } from '../../Services/User/user.service';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../Services/Security/auth.service';

@Component({
  selector: 'app-account',
  imports: [ReactiveFormsModule, CommonModule, FormsModule],
  templateUrl: './account.component.html',
  styleUrl: './account.component.css',
})
export class AccountComponent implements OnInit {
  public user!: User;
  updateError = '';
  updateSuccess = '';

  constructor(
    private userService: UserService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    const currentUser = this.authService.currentUserValue;

    if (currentUser) {
      this.getUserById(currentUser.id);
    } else {
      console.error('Bruger ikke logget ind!');
      // Du kan evt. redirecte til login her, hvis ønsket
    }
  }

  getUserById(id: number): void {
    this.userService.getUserById(id).subscribe(
      (user) => {
        this.user = user;
        console.log('Bruger hentet:', this.user);
      },
      (error) => {
        console.error('Fejl ved hentning af bruger:', error);
      }
    );
  }

  updateUser(): void {
    if (!this.user) return;
  
    this.userService.updateUser(this.user).subscribe({
      next: (updatedUser) => {
        this.user = { ...this.user, ...updatedUser };       
        this.updateSuccess = 'Din profil er blevet opdateret!';
        this.updateError = '';
        console.log('Bruger opdateret:', updatedUser);
      },
      error: (err) => {
        this.updateSuccess = '';
        this.updateError = 'Der opstod en fejl ved opdatering af profilen.';
        console.error(err);
      },
    });
  }
}  
