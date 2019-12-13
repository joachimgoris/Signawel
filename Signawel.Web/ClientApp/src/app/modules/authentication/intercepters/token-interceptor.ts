import { Injectable } from "@angular/core";
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from "@angular/common/http";
import { Observable, BehaviorSubject, throwError } from "rxjs";
import { tap, filter, switchMap, take, catchError } from "rxjs/operators";
import { AuthenticationService } from "../services/authentication.service";
import { TokenModel } from "../models/token.model";
import { GIPOD_BASE_URL } from 'src/app/constants/api.constants';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  private isRefreshingToken: boolean = false;
  private refreshTokenSubject: BehaviorSubject<
    TokenModel
  > = new BehaviorSubject<TokenModel>(null);

  constructor(private authService: AuthenticationService) {}

  private addToken(request: HttpRequest<any>, token: string): HttpRequest<any> {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if(request.url.includes(GIPOD_BASE_URL)) {
      return next.handle(request);
    }

    if (this.authService.getJwtToken()) {
      request = this.addToken(request, this.authService.getJwtToken());
    }

    return next.handle(request).pipe(
      catchError(error => {
        if (
          request.url.includes("authentication/login") ||
          request.url.includes("authentication/refresh")
        ) {
          if (request.url.includes("authentication/refresh")) {
            this.authService.logout();
          }

          return throwError(error);
        }

        if (error instanceof HttpErrorResponse && error.status === 401) {
          return this.handle401Error(request, next);
        } else {
          return throwError(error);
        }
      })
    );
  }

  private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
    if (!this.isRefreshingToken) {
      this.isRefreshingToken = true;
      this.refreshTokenSubject.next(null);

      return this.authService.attemptRefreshToken().pipe(
        switchMap((token: TokenModel) => {
          this.isRefreshingToken = false;
          this.refreshTokenSubject.next(token);
          return next.handle(this.addToken(request, token.token));
        })
      );
    } else {
      return this.refreshTokenSubject.pipe(
        filter(token => token != null),
        take(1),
        switchMap((token: TokenModel) => {
          return next.handle(this.addToken(request, token.token));
        })
      );
    }
  }
}
