import { Injectable } from '@angular/core';
import {HttpInterceptor,HttpRequest,HttpHandler,HttpEvent,} from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokenService } from '../Security/token.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private tokenService: TokenService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const token = this.tokenService.getToken();
    
    if (token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`,
        },
      });
    }

    return next.handle(request);
  }
}
