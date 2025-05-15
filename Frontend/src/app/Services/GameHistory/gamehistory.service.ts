import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../Environments/environment';

@Injectable({
  providedIn: 'root' // Gør servicen tilgængelig globalt
})
export class GamehistoryService {

  private readonly gamehistoryApiUrl = environment.apiUrl + '/GameHistory';

  constructor(private http: HttpClient) {}

  getGameHistory(): Observable<any> {
    return this.http.get<any>(this.gamehistoryApiUrl);
  }
}
