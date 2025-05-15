import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../Environments/environment';
import { DiceGame } from '../../Models/dicegame.model';
import { Observable, throwError } from 'rxjs';
import { AuthService } from '../Security/auth.service';

@Injectable({ providedIn: 'root' })
export class DiceService {
  private apiUrl = environment.apiUrl + '/Dice/playGame';

  constructor(private http: HttpClient, private authService: AuthService) {}

  playGame(playerNumber: number, isGuessOver: boolean, betAmount: number): Observable<DiceGame> {
    const currentUser = this.authService.currentUserValue;
    const userId = currentUser?.id;
    const gameId = 1;

    if (!userId) {
      return throwError(() => new Error('Bruger ikke logget ind.'));
    }

    const body = {
      userId,
      gameId,
      playerNumber,
      isGuessOver,
      betAmount
    };

    return this.http.post<DiceGame>(this.apiUrl, body);
  }
}
