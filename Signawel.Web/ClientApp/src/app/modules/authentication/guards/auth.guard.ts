import { AuthenticationService } from "../services/authentication.service";
import { Router } from "@angular/router";
import { Injectable } from "@angular/core";

@Injectable()
export class AuthenticationGuard {
  constructor(
    private router: Router,
    private authService: AuthenticationService
  ) {}

  canActivate() {
    if (this.authService.isLoggedIn()) {
      return true;
    }

    this.router.navigate(["authentication/login"]);
    return false;
  }
}
