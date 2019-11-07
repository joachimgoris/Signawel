import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LayoutComponent } from './components/layout/layout.component';
import { HomeComponent } from './components/home/home.component';
import { ButtonBarComponent } from "./components/shared/button-bar/button-bar/button-bar.component";
import { ButtonBarButtonComponent } from "./components/shared/button-bar/button-bar-button/button-bar-button.component";

@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent,
    HomeComponent,
    ButtonBarComponent,
    ButtonBarButtonComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
	HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
