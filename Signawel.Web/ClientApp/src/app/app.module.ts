import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { LayoutModule } from "@angular/cdk/layout";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { LayoutComponent } from "./components/layout/layout.component";
import { HomeComponent } from "./components/home/home.component";
import { AuthenticationModule } from "./modules/authentication/authentication.module";
import { DeterminationModule } from "./modules/determination/determination.module";
import { ReportGroupsModule } from "./modules/report-groups/report-groups.module";
import { SignawelMaterialModule } from "./modules/signawel-material/signawel-material.module";
import { SharedModule } from "./modules/shared/shared.module";

@NgModule({
  declarations: [AppComponent, LayoutComponent, HomeComponent],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    LayoutModule,
    HttpClientModule,
    AppRoutingModule,
    SignawelMaterialModule,
    SharedModule,
    AuthenticationModule,
    ReportGroupsModule,
    DeterminationModule,
    ReportGroupsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
