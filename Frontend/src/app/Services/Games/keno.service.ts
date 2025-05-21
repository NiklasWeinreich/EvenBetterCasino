import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../Environments/environment';
import { KenoGetOdds } from '../../Models/Keno/keno.getodds';
import { kenoGetRandomNumbers } from '../../Models/Keno/keno.getrandomplayernumbers';
import { kenoPlayGame } from '../../Models/Keno/keno.playgame';

@Injectable({
  providedIn: 'root',
})
export class KenoService {
  private getOddsUrl = environment.apiUrl + '/Keno/getOdds';
  private getRandomPlayerNumbersUrl = environment.apiUrl + '/Keno/getRandomPlayerNumbers';
  private playGameUrl = environment.apiUrl + '/Keno/playGame';

  constructor(private http: HttpClient) {}

  getOdds(kenoRequest: KenoGetOdds) {
    return this.http.post<{ playerNumbers: number; multiplier: number }[]>(this.getOddsUrl, kenoRequest);
  }

  getRandomPlayerNumbers(userId: number, gameId: number, betAmount: number) {
    const body = { userId, gameId, betAmount };
    return this.http.post<kenoGetRandomNumbers>(this.getRandomPlayerNumbersUrl, body);
  }

  playGame(kenoRequest: KenoGetOdds) {
    return this.http.post<kenoPlayGame>(this.playGameUrl, kenoRequest);
  }
}
