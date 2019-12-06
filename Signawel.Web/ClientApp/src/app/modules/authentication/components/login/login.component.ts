import { Component, OnInit } from "@angular/core";
import { MatSnackBar } from "@angular/material";
import { Router } from "@angular/router";
import { AuthenticationService } from "../../services/authentication.service";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.sass"]
})
export class LoginComponent implements OnInit {
  private isLoading: boolean;

  constructor(
    private authenticationService: AuthenticationService,
    private router: Router,
    private matSnackbar: MatSnackBar
  ) {}

  ngOnInit() {}

  onSubmit(formData: { email: string; password: string }) {
    this.isLoading = true;
    this.authenticationService
      .login(formData["email"], formData["password"])
      .subscribe(
        result => {
          if(this.authenticationService.getIsAdmin()) {
            this.router.navigate(["/"]);
          } else {
            this.authenticationService.logout();
            this.matSnackbar.open(
              "U heeft niet voldoende rechten om te kunnen aanmelden.",
              null,
              { duration: 3000 }
            );
          }
        },
        error => {
          this.matSnackbar.open(
            "Het e-mailadres en/of wachtwoord is onjuist.",
            null,
            { duration: 3000 }
          );
        }
      )
      .add(() => {
        this.isLoading = false;
      });
  }
}
