import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import {
  AUTHENTICATE_REFRESH_URL,
  AUTHENTICATE_LOGIN_URL
} from "src/app/constants/api.constants";
import { tap, catchError, mapTo } from "rxjs/operators";
import { TokenModel } from "../models/token.model";
import { Observable, of, throwError } from "rxjs";
import * as jwt_decode from 'jwt-decode';
import { UserModel } from '../models/user.model';

@Injectable()
export class AuthenticationService {
  private readonly JWT_TOKEN = "JWT_TOKEN";
  private readonly REFRESH_TOKEN = "REFRESH_TOKEN";
  private readonly CURRENT_USER = "CURRENT_USER";

  constructor(private httpClient: HttpClient, private router: Router) { }

  login(email: string, password: string) {
    return this.httpClient
      .post<TokenModel>(AUTHENTICATE_LOGIN_URL, {
        email: email,
        password: password
      })
      .pipe(
        tap(tokenModel => {
          var decoded = jwt_decode(tokenModel.token);
          const userModel = new UserModel(
            decoded['user_id'],
            decoded['email'],
            decoded['role'] === 'Admin'
          );
          this.doLoginuser(tokenModel, userModel);
        }),
        mapTo(true),
        catchError(error => {
          console.error(error);
          return throwError(error);
        })
      );
  }

  logout() {
    this.removeTokens();
    this.router.navigate(["/authentication/login"]);
  }

  isLoggedIn() {
    return !!this.getJwtToken();
  }

  getJwtToken() {
    return localStorage.getItem(this.JWT_TOKEN);
  }

  getCurrentUser(): UserModel {
    return JSON.parse(localStorage.getItem(this.CURRENT_USER));
  }

  getIsAdmin(): boolean {
    return this.getCurrentUser().isAdmin;
  }

  attemptRefreshToken(): Observable<TokenModel> {
    return this.httpClient
      .post<TokenModel>(AUTHENTICATE_REFRESH_URL, {
        jwtToken: this.getJwtToken(),
        refreshToken: this.getRefreshToken()
      })
      .pipe(
        tap((tokens: TokenModel) => {
          this.storeTokens(tokens);
        })
      );
  }

  private getRefreshToken() {
    return localStorage.getItem(this.REFRESH_TOKEN);
  }

  private doLoginuser(tokens: TokenModel, userModel: UserModel) {
    this.storeTokens(tokens);
    this.storeUser(userModel);
  }

  private storeTokens(tokens: TokenModel) {
    localStorage.setItem(this.JWT_TOKEN, tokens.token);
    localStorage.setItem(this.REFRESH_TOKEN, tokens.refreshToken);
  }

  private storeUser(userModel: UserModel) {
    localStorage.setItem(this.CURRENT_USER, JSON.stringify(userModel));
  }

  private removeTokens() {
    localStorage.removeItem(this.JWT_TOKEN);
    localStorage.removeItem(this.REFRESH_TOKEN);
  }
}
