import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { HTTP_INTERCEPTORS } from "@angular/common/http";

import { AuthenticationRoutingModule } from "./authentication-routing.module";
import { LoginComponent } from "./components/login/login.component";
import { AuthenticationGuard } from "./guards/auth.guard";
import { TokenInterceptor } from "./intercepters/token-interceptor";
import { AuthenticationService } from "./services/authentication.service";
import { SignawelMaterialModule } from "../signawel-material/signawel-material.module";

@NgModule({
  declarations: [LoginComponent],
  imports: [
    CommonModule,
    FormsModule,
    SignawelMaterialModule,
    AuthenticationRoutingModule
  ],
  providers: [
    AuthenticationGuard,
    AuthenticationService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ]
})
export class AuthenticationModule {}
