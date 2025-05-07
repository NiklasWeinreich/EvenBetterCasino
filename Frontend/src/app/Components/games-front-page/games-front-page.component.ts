import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-games-front-page',
  imports: [CommonModule, HttpClientModule, FormsModule, RouterLink],
  templateUrl: './games-front-page.component.html',
  styleUrl: './games-front-page.component.css'
})
export class GamesComponent {

}
