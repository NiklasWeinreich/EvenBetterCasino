import { Injectable } from '@angular/core';
import { environment } from '../Environments/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { User } from '../Models/user.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly userApiUrl = environment.apiUrl + '/User';

  constructor(private http: HttpClient) {}

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.userApiUrl}`)
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    console.error('An error occurred:', error.message);
    return throwError(() => new Error(error.message || 'Server error'));
  }
}
