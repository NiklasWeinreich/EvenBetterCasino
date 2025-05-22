import { Injectable } from '@angular/core';
import { environment } from '../../Environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Game } from '../../Models/games.model';

@Injectable({
  providedIn: 'root',
})
export class GameService {
  private readonly gamesApiUrl = environment.apiUrl + '/Games';

  constructor(private http: HttpClient) {}

  getAllGames(): Observable<Game[]> {
    return this.http.get<Game[]>(this.gamesApiUrl);
  }
}
