import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../Environments/environment';

@Injectable({ providedIn: 'root' })
export class BombasticService {
  private apiUrl = environment.apiUrl + '/BombasticGame';

  constructor(private http: HttpClient) {}

  startGame(betAmount: number) {
    return this.http.post<{ sessionId: string, message: string }>(`${this.apiUrl}/startGame`, { betAmount });
  }

  clickBomb(sessionId: string) {
    return this.http.post<any>(`${this.apiUrl}/clickBomb`, { sessionId });
  }
}
