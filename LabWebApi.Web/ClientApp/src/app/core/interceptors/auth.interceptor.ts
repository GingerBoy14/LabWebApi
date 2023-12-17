import {
  HTTP_INTERCEPTORS,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';

import { AuthenticationService } from '../services/Authentication.service';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(
    private authService: AuthenticationService,
    private router: Router
  ) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((err) => {
        if (err.status == 401 && err.headers.get('token-expired')) {
          return this.handle401Error(req, next);
        }
        return throwError(() => err);
      })
    );
  }
  private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
    return this.authService.refreshToken().pipe(
      switchMap(() => {
        const token = localStorage.getItem('token')?.toString();
        return next.handle(this.addTokenHeader(request, token));
      }),
      catchError((err) => {
        if (err.error.error == 'Invalid refrash token') {
          this.authService.logout();
          this.router.navigate(['/login']);
        }

        return throwError(() => err);
      })
    );
  }
  private addTokenHeader(request: HttpRequest<any>, token?: string) {
    return request.clone({
      headers: request.headers.set('Authorization', 'Bearer ' + token),
    });
  }
}
export const AuthInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: AuthInterceptor,
  multi: true,
};
