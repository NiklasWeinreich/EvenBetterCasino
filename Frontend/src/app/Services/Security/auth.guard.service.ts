import { Injectable } from '@angular/core';
import {CanActivate,Router,} from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  canActivate(): boolean {
    const currentUser = this.authService.currentUserValue;
    if (currentUser) {
      // Logget ind => OK
      return true;
    }
    // Ikke logget ind => Sæt en global besked + redirect (eller ej)
    this.authService.setGlobalMessage('Du skal være logget ind først!');
    // Evt. redirect til login her, hvis du stadig vil omdirigere
    this.router.navigate(['/login']);
    return false;
  }
}