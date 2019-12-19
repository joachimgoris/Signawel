import { Component, OnInit } from "@angular/core";
import {
  BreakpointObserver,
  BreakpointState,
  Breakpoints
} from "@angular/cdk/layout";
import { Observable } from "rxjs";
import { AuthenticationService } from "src/app/modules/authentication/services/authentication.service";
import { Router } from "@angular/router";

@Component({
  selector: "app-layout",
  templateUrl: "./layout.component.html",
  styleUrls: ["./layout.component.sass"]
})
export class LayoutComponent implements OnInit {
  isHandset: Observable<BreakpointState> = this.breakpointObserver.observe(
    Breakpoints.Handset
  );

  public IsAdmin: boolean;

  constructor(
    private breakpointObserver: BreakpointObserver,
    private authService: AuthenticationService,
    private router: Router
  ) {}

  ngOnInit() {
    this.IsAdmin = false;
    
    if(this.authService.getCurrentUser() == null){
      this.logout();
    }
    
    if(this.authService.getCurrentUser().isAdmin){
      this.IsAdmin = true;
    }
  }

  logout() {
    this.authService.logout();
    this.router.navigate(["/authentication/login"]);
  }
}
