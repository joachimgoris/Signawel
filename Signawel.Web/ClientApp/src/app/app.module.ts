import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { LayoutComponent } from "./components/layout/layout.component";
import { HomeComponent } from "./components/home/home.component";
import { DeterminationGraphComponent } from "./components/determination-graph/determination-graph/determination-graph.component";
import { QuestionNodeComponent } from "./components/determination-graph/question-node/question-node.component";
import { ButtonBarComponent } from "./components/shared/button-bar/button-bar/button-bar.component";
import { ButtonBarButtonComponent } from "./components/shared/button-bar/button-bar-button/button-bar-button.component";
import { DeterminationEndpointDetailComponent } from './components/determination-graph/determination-endpoint/determination-endpoint-detail/determination-endpoint-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent,
    HomeComponent,
    DeterminationGraphComponent,
    QuestionNodeComponent,
    ButtonBarComponent,
    ButtonBarButtonComponent,
    DeterminationEndpointDetailComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
