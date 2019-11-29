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

  constructor(
    private breakpointObserver: BreakpointObserver,
    private authService: AuthenticationService,
    private router: Router
  ) {}

  ngOnInit() {}

  logout() {
    this.authService.logout();
    this.router.navigate(["/authentication/login"]);
  }
}
