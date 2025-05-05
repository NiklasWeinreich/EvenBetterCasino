import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  private storage: Storage | null = null;

  constructor() {
    if (typeof window !== 'undefined') {
      this.storage = sessionStorage; // sessionStorage findes kun i browseren
    }
  }

  setToken(token: string, rememberMe: boolean) {
      if (!this.storage) return;
      if (rememberMe) {
        localStorage.setItem('token', token);
      } else {
        this.storage.setItem('token', token);
      }
    }
    
    getToken(): string | null {
      if (!this.storage) return null;
      return localStorage.getItem('token') || this.storage.getItem('token');
    }
    
    removeToken() {
      if (!this.storage) return;
      localStorage.removeItem('token');
      this.storage.removeItem('token');
  }
        
}