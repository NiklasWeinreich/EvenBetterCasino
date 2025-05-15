import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../Environments/environment';
import { AuthService } from '../Security/auth.service';
import { resetUser, User } from '../../Models/user.model';

@Injectable({
  providedIn: 'root' // Gør servicen tilgængelig globalt
})
export class GamehistoryService {

  private readonly gamehistoryApiUrl = environment.apiUrl + '/GameHistory';



  constructor(private http: HttpClient) {}





  getGameHistory(): Observable<any> {
    return this.http.get<any>(this.gamehistoryApiUrl);
  }
  
  getGameHistoryByUserId(userId: number): Observable<any> {
    return this.http.get<any>(`${this.gamehistoryApiUrl}/user/${userId}`);
  }

  getGameHistoryByGameId(gameId: number): Observable<any> {
    return this.http.get<any>(`${this.gamehistoryApiUrl}/game/${gameId}`);
  }

  getGameHistoryByUserIdAndGameId(userId: number, gameId: number): Observable<any> {
    return this.http.get<any>(`${this.gamehistoryApiUrl}/user/${userId}/game/${gameId}`);
  }





}
