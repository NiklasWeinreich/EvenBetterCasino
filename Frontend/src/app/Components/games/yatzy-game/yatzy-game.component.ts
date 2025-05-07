import { Component } from '@angular/core';
import { environment } from '../../../Environments/environment';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-yatzy-game',
  imports: [FormsModule, CommonModule],
  templateUrl: './yatzy-game.component.html',
  styleUrl: './yatzy-game.component.css'
})
export class YatzyGameComponent {

   // strings
   result: string = "";
   sessionId: string = '';
 
   // ints
   betAmount: number = 0;
   currentClickNumber: number = 0;
   currentMultiplier: number = 0.00;
   currentWinAmount: number = 0.00;
     
   // bools
   bombClicked = false;  // Variabel til at styre animationen
   betButton: boolean = true
   bombButton: boolean = false
   isBombButtonActive: boolean = false;
   isBet: boolean = true; 
   isExploded: boolean = false
   inputField: boolean = true
   validBalance: boolean = true
   notZero: boolean = true;
 
   apiUrl: string = environment.apiUrl 
   



}
