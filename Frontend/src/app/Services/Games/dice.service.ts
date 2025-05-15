import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../Environments/environment';
import { DiceGame } from '../../Models/dicegame.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class DiceService {
  private apiUrl = environment.apiUrl + '/Dice/playGame';

  constructor(private http: HttpClient) {}

  playGame(playerNumber: number, isGuessOver: boolean, betAmount: number): Observable<DiceGame> {
    const body = { playerNumber, isGuessOver, betAmount };
    return this.http.post<DiceGame>(this.apiUrl, body);
  }
}
