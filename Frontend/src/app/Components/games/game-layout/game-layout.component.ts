import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-game-layout',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule],
  templateUrl: './game-layout.component.html',
  styleUrls: ['./game-layout.component.css']
})

export class GameLayoutComponent {
  betAmount = 0;

  placeBet() {
    console.log('Indsat beløb:', this.betAmount);
  }
}