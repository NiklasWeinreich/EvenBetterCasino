import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-game-layout',
  standalone: true,
  imports: [FormsModule], // 👈 her!
  templateUrl: './game-layout.component.html',
  styleUrls: ['./game-layout.component.css']
})

export class GameLayoutComponent {
  betAmount = 0;

  placeBet() {
    console.log('Indsat beløb:', this.betAmount);
  }
}