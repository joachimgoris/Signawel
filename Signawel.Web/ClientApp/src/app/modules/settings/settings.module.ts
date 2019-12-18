import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { SettingsRoutingModule } from "./settings-routing.module";
import { SettingsPageComponent } from "./components/settings-page/settings-page.component";
import { PriorityEmailsComponent } from "./components/priority-emails/priority-emails.component";
import { DefaultIssuesComponent } from "./components/default-issues/default-issues.component";
import { SharedModule } from "../shared/shared.module";
import { SignawelMaterialModule } from "../signawel-material/signawel-material.module";
import { FormsModule } from "@angular/forms";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { TokenInterceptor } from "../authentication/intercepters/token-interceptor";
import { BlacklistEmailsComponent } from './components/blacklist-emails/blacklist-emails.component';

@NgModule({
  declarations: [
    SettingsPageComponent,
    PriorityEmailsComponent,
    DefaultIssuesComponent,
    BlacklistEmailsComponent
  ],
  imports: [
    CommonModule,
    SettingsRoutingModule,
    SharedModule,
    SignawelMaterialModule,
    FormsModule
  ]
})
export class SettingsModule {}
