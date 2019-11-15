import { Injectable, Injector } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from '@angular/common/http';
import { Observable, BehaviorSubject, throwError } from 'rxjs';
import { tap, filter, switchMap, take, catchError } from 'rxjs/operators';
import { AuthenticationService } from '../services/authentication/authentication.service';
import { TokenModel } from '../models/authentication/token.model';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
    private authService: AuthenticationService;

    isRefreshingToken: boolean = false;
    refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

    constructor(private injector: Injector) {
        this.authService = injector.get(AuthenticationService);
    }

    private addToken(request: HttpRequest<any>): HttpRequest<any> {
        let token: string | null = this.authService.getJwtToken();

        if (token == null) {
            return request;
        }

        return request.clone({
            setHeaders: {
                Authorization: `Bearer ${token}`
            }
        });
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        request = this.addToken(request)

        return next.handle(request).pipe(
            tap(null, (error: any) => {
                if (request.url.includes('authentication/login') || request.url.includes('authentication/refresh')) {

                    if (request.url.includes('authentication/refresh')) {
                        this.authService.logout();
                    }

                    return throwError(error);
                }

                if (error.status !== 401) {
                    return throwError(error);
                }

                if (this.isRefreshingToken) {
                    return this.refreshTokenSubject.pipe(
                        filter(result => result != null),
                        take(1),
                        switchMap(() => next.handle(this.addToken(request)))
                    );
                } else {
                    this.isRefreshingToken = true;
                    this.refreshTokenSubject.next(null);

                    return this.authService.attemptRefreshToken().pipe(
                        switchMap((tokenModel: TokenModel) => {
                            this.isRefreshingToken = false;
                            this.refreshTokenSubject.next(tokenModel.jwt);

                            return next.handle(this.addToken(request));
                        })
                    );
                }
            })
        );
    }
}