import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { GameLayoutComponent } from '../game-layout/game-layout.component';

@Component({
  selector: 'app-dice-game',
  imports: [FormsModule, GameLayoutComponent],
  templateUrl: './dice-game.component.html'
})
export class DiceGameComponent {
  selectedNumber = 50;
}
