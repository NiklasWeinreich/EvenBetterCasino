import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, switchMap, throwError } from 'rxjs';
import { User } from '../../Models/user.model';
import { HttpClient } from '@angular/common/http';
import { TokenService } from './token.service';
import { environment } from '../../Environments/environment';
import { LoginResponse } from '../../Models/loginresponse.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<User | null>;
  public currentUser: Observable<User | null>;

  private globalMessageSubject = new BehaviorSubject<string | null>(null);
  public globalMessage$ = this.globalMessageSubject.asObservable();

  constructor(private http: HttpClient, private tokenService: TokenService) {
    this.currentUserSubject = new BehaviorSubject<User | null>(null);
    this.currentUser = this.currentUserSubject.asObservable();

    const storedToken = this.tokenService.getToken();
    if (storedToken) {
      const storedBasicUserInfo =
        localStorage.getItem('basicUserInfo') ||
        sessionStorage.getItem('basicUserInfo');

      if (storedBasicUserInfo) {
        try {
          const loginResp: LoginResponse = JSON.parse(storedBasicUserInfo);
          this.getUserDetails(loginResp.id).subscribe({
            next: (fullUser) => this.currentUserSubject.next(fullUser),
            error: () => this.logout(),
          });
        } catch (error) {
          console.error('Fejl ved parsing af basicUserInfo:', error);
        }
      }
    }
  }

  public get currentUserValue(): User | null {
    return this.currentUserSubject.value;
  }

  public setGlobalMessage(message: string | null) {
    this.globalMessageSubject.next(message);
  }

  public get tokenValue(): string | null {
    return this.tokenService.getToken();
  }

  public login(email: string, password: string, rememberMe: boolean) {
    const authenticateUrl = `${environment.apiUrl}/Auth/login`;

return this.http
  .post<LoginResponse>(authenticateUrl, { email, password })
  .pipe(
    switchMap((loginResp) => {
      return this.getUserDetails(loginResp.id).pipe(
        switchMap((fullUser) => {
          if (
            fullUser.excludedUntil &&
            new Date(fullUser.excludedUntil) > new Date()
          ) {
            const msg =
              'Din konto er midlertidigt udelukket indtil ' +
              new Date(fullUser.excludedUntil).toLocaleString();
            this.logout(); // rydder også storage
            this.setGlobalMessage(msg);
            return throwError(() => new Error(msg));
          }

          this.tokenService.setToken(loginResp.token, rememberMe);
          const storage = rememberMe ? localStorage : sessionStorage;
          storage.setItem('basicUserInfo', JSON.stringify(loginResp));

          this.currentUserSubject.next(fullUser);
          return new Observable<LoginResponse>((observer) => {
            observer.next(loginResp);
            observer.complete();
          });
        })
      );
    })
  );
  }

  public logout() {
    this.tokenService.removeToken();
    localStorage.removeItem('basicUserInfo');
    sessionStorage.removeItem('basicUserInfo');
    this.currentUserSubject.next(null);
  }

  public getUserDetails(userId: number): Observable<User> {
    return this.http.get<User>(`${environment.apiUrl}/User/${userId}`);
  }

  public registerUser(newUser: User): Observable<User> {
    const registerUrl = `${environment.apiUrl}/Auth/register`;
    return this.http.post<User>(registerUrl, newUser);
  }
}
