import { Component } from '@angular/core';
import { environment } from '../../../Environments/environment';
import { FormsModule } from '@angular/forms';
import { CommonModule, NgClass } from '@angular/common';
import { GameLayoutComponent } from "../game-layout/game-layout.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-yatzy-game',
  imports: [FormsModule, CommonModule, GameLayoutComponent],
  templateUrl: './yatzy-game.component.html',
  styleUrl: './yatzy-game.component.css'
})
export class YatzyGameComponent {

  

}