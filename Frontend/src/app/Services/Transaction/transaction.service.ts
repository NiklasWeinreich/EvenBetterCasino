import { Injectable } from '@angular/core';
import { environment } from '../../Environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {



  private readonly transactionApiUrl = environment.apiUrl + '/Transaction';

  constructor(private http: HttpClient) { }

  getTransactionTicketsByUserId(userId: number): Observable<any> {
    return this.http.get<any>(`${this.transactionApiUrl}/user/${userId}`);
  }
}