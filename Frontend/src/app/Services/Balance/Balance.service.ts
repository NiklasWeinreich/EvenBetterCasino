import { Injectable } from "@angular/core";
import { environment } from "../../Environments/environment";
import { Balance } from "../../Models/Balance.model";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root',
  })
  export class BalanceService {
    private readonly BalanceUrl = environment.apiUrl + '/Balance';
  
    constructor(private http: HttpClient) {}
  
    getBalance(userId: number): Observable<Balance> {
      return this.http.get<Balance>(`${this.BalanceUrl}/${userId}/get`);
    }
  
    depositMoney(userId: number, amount: number): Observable<Balance> {
      return this.http.post<Balance>(`${this.BalanceUrl}/${userId}/deposit`, { amount });
    }
  
    withdrawMoney(userId: number, amount: number): Observable<Balance> {
      return this.http.post<Balance>(`${this.BalanceUrl}/${userId}/withdraw`, { amount });
    }
  }