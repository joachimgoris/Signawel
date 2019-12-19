import { AuthenticationService } from "../services/authentication.service";
import { Injectable } from "@angular/core";

@Injectable()
export class AuthenticationGuard {
  constructor(private authService: AuthenticationService) {}

  canActivate() {
    if (this.authService.isLoggedIn() && (this.authService.getIsAdmin() || this.authService.getIsInstance())) {
      return true;
    }

    this.authService.logout();
    return false;
  }
}
