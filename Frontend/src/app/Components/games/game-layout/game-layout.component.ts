import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../../Services/Security/auth.service';
import { User } from '../../../Models/user.model';


@Component({
  selector: 'app-game-layout',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule],
  templateUrl: './game-layout.component.html',
  styleUrls: ['./game-layout.component.css'],
})
export class GameLayoutComponent {
  @Input() betAmount!: number;
  @Output() betPlaced = new EventEmitter<number>();


    constructor(private authService: AuthService) {}

    currentUser?: User | null; // Tilføj denne property



  ngOnInit() {
    this.authService.currentUser.subscribe(user => {
      this.currentUser = user;
    });
  }
  
  placeBet() {
    if (this.betAmount < 1) {
      alert('Indsats skal være mindst 1 kr');
      return;
    }
    this.betPlaced.emit(this.betAmount);
  }
}
