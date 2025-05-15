import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

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

  placeBet() {
    if (this.betAmount < 1) {
      alert('Indsats skal være mindst 1 kr');
      return;
    }
    this.betPlaced.emit(this.betAmount);
  }
}
