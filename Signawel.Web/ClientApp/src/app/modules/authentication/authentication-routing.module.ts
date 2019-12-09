import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { LoginComponent } from "./components/login/login.component";
import { ConfirmEmailComponent } from './components/confirm-email/confirm-email.component';

const routes: Routes = [
  { path: "login", component: LoginComponent },
  { path: "confirm-email", component: ConfirmEmailComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthenticationRoutingModule { }
