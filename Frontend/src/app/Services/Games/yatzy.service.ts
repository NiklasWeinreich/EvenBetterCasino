import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { Game } from "../../Models/games.model";
import { environment } from "../../Environments/environment";
import { YatzyGame } from "../../Models/Yatzy.model";
import { AuthService } from "../Security/auth.service";

@Injectable({
    providedIn: 'root',
})
export class YatzyService {
  private apiUrl = environment.apiUrl + '/YatzyGame/playGame';

  constructor(private http: HttpClient, private authService: AuthService) {}

  playGame(betAmount: number): Observable<YatzyGame> {
    const currentUser = this.authService.currentUserValue;
    const userId = currentUser?.id;
    const gameId = 1;

    if (!userId) {
      return throwError(() => new Error('Bruger ikke logget ind.'));
    }

    const body = {
      userId,
      gameId,
      betAmount
    };

    return this.http.post<YatzyGame>(this.apiUrl, body);
  }
}