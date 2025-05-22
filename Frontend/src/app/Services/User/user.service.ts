import { Injectable } from '@angular/core';
import { environment } from '../../Environments/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { User } from '../../Models/user.model';
import { resetPassword } from '../../Models/resetpassword.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly userApiUrl = environment.apiUrl + '/User';

  constructor(private http: HttpClient) { }

  getUserById(id: number): Observable<User> {
    return this.http
      .get<User>(`${this.userApiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  getAllUsers(): Observable<User[]> {
    return this.http
      .get<User[]>(`${this.userApiUrl}`)
      .pipe(catchError(this.handleError));
  }

  deleteUser(id: number): Observable<void> {
    return this.http
      .delete<void>(`${this.userApiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  createUser(user: User): Observable<User> {
    console.log('Opretter bruger:', user);
    const formData = new FormData();

    formData.append('firstName', user.firstName);
    formData.append('lastName', user.lastName);
    formData.append('email', user.email);
    formData.append('phoneNumber', user.phoneNumber.toString());
    formData.append('password', user.password);
    formData.append(
      'newsLetterIsSubscribed',
      String(user.newsLetterIsSubscribed)
    );

    if (user.role) {
      formData.append('role', user.role);
    }

    return this.http.post<User>(this.userApiUrl + '/register', formData);
  }

  updateUser(user: User): Observable<User> {
    const formData = new FormData();

    formData.append('firstName', user.firstName);
    formData.append('lastName', user.lastName);
    formData.append('email', user.email);
    formData.append('phoneNumber', user.phoneNumber.toString());
    formData.append(
      'newsLetterIsSubscribed',
      String(user.newsLetterIsSubscribed)
    );
    formData.append('birthDate', user.birthDate.toString());

    if (user.password && user.password.trim() !== '') {
      formData.append('password', user.password);
    }

    if (user.role) {
      formData.append('role', user.role);
    }

    return this.http.put<User>(
      `${this.userApiUrl}/${user.id}/update`,
      formData
    );
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    console.error('An error occurred:', error.message);
    return throwError(() => new Error(error.message || 'Server error'));
  }

  excludeUser(userId: number, hours: number): Observable<any> {
    return this.http.post(`${this.userApiUrl}/${userId}/exclude`, hours);
  }

  subscribe(userMail: string): Observable<User> {
    return this.http.post<User>(this.userApiUrl + '/Newsletter/Subscribe/' + userMail, userMail);
  }

  unsubscribe(userMail: string): Observable<User> {
    return this.http.post<User>(this.userApiUrl + '/Newsletter/Unsubscribe/' + userMail, userMail);
  }

  forgotPassword(email: string): Observable<any> {
  return this.http.post(`${this.userApiUrl}/forgot-password`, { email });
  }
  
  resetPassword(resetData: resetPassword): Observable<any> {
  return this.http.post(`${this.userApiUrl}/reset-password`, resetData);
}

}
