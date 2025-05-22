import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../Environments/environment';
import { AuthService } from '../Security/auth.service';
import { bombasticGame } from '../../Models/bombastic.model';
import { throwError } from 'rxjs/internal/observable/throwError';

@Injectable({ providedIn: 'root' })
export class BombasticService {
  private apiUrl = environment.apiUrl + '/BombasticGame';

  constructor(private http: HttpClient, private authService: AuthService) { }

startGame(betAmount: number) {
  const currentUser = this.authService.currentUserValue;
  const userId = currentUser?.id;
  const gameId = 3; // eller det relevante spil-ID

  if (!userId) {
    return throwError(() => new Error('Bruger ikke logget ind.'));
  }

  const body = {
    userId,
    gameId,
    betAmount
  };

  return this.http.post<bombasticGame>(`${this.apiUrl}/startGame`, body);
}



  clickBomb(sessionId: string) {
    return this.http.post<bombasticGame>(`${this.apiUrl}/clickBomb`, { sessionId });
  }
}

