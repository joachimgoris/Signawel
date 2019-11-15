import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AUTHENTICATE_REFRESH_URL, AUTHENTICATE_LOGIN_URL } from 'src/app/constants/api.constants';
import { tap } from 'rxjs/operators';
import { TokenModel } from 'src/app/models/authentication/token.model';

@Injectable()
export class AuthenticationService {
    private readonly JWT_TOKEN = 'JWT_TOKEN';
    private readonly REFRESH_TOKEN = 'REFRESH_TOKEN';

    constructor(private httpClient: HttpClient) { }

    login(email: string, password: string) {
        return this.httpClient.post<any>(AUTHENTICATE_LOGIN_URL, {
            'email': email,
            'password': password
        }).pipe(tap(tokens => {
            this.doLoginuser({ jwt: tokens.token, refresh: tokens.refreshToken });
        }));
    }

    logout() {
        this.removeTokens();
    }

    isLoggedIn() {
        return !!this.getJwtToken();
    }

    getJwtToken() {
        return localStorage.getItem(this.JWT_TOKEN);
    }

    attemptRefreshToken() {
        return this.httpClient.post<any>(AUTHENTICATE_REFRESH_URL, {
            'refreshToken': this.getRefreshToken()
        }).pipe(tap((tokens: TokenModel) => {
            this.storeJwtToken(tokens.jwt);
        }));
    }

    private storeJwtToken(jwt: string) {
        localStorage.setItem(this.JWT_TOKEN, jwt);
    }

    private getRefreshToken() {
        return localStorage.getItem(this.REFRESH_TOKEN);
    }

    private doLoginuser(tokens: TokenModel) {
        this.storeTokens(tokens);
    }

    private storeTokens(tokens: TokenModel) {
        localStorage.setItem(this.JWT_TOKEN, tokens.jwt);
        localStorage.setItem(this.REFRESH_TOKEN, tokens.refresh);
    }

    private removeTokens() {
        localStorage.removeItem(this.JWT_TOKEN);
        localStorage.removeItem(this.REFRESH_TOKEN);
    }
}