import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
import {
  MatTableModule,
  MatPaginatorModule,
  MatSortModule,
  MatProgressSpinnerModule,
  MatInputModule,
  MatCheckboxModule,
  MatMenuModule,
  MatToolbarModule,
  MatButtonModule,
  MatSidenavModule,
  MatIconModule,
  MatListModule
} from "@angular/material";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { LayoutModule } from "@angular/cdk/layout";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { LayoutComponent } from "./components/layout/layout.component";
import { HomeComponent } from "./components/home/home.component";
import { DeterminationGraphComponent } from "./components/determination-graph/determination-graph/determination-graph.component";
import { QuestionNodeComponent } from "./components/determination-graph/question-node/question-node.component";
import { ButtonBarComponent } from "./components/shared/button-bar/button-bar/button-bar.component";
import { ButtonBarButtonComponent } from "./components/shared/button-bar/button-bar-button/button-bar-button.component";
import { DeterminationEndpointDetailComponent } from "./components/determination-graph/determination-endpoint/determination-endpoint-detail/determination-endpoint-detail.component";
import { DeterminationEndpointListComponent } from "./components/determination-graph/determination-endpoint/determination-endpoint-list/determination-endpoint-list.component";
import { LoaderComponent } from "./components/shared/loader/loader.component";
import { BladeModalComponent } from "./components/shared/blade-modal/blade-modal.component";
import { BoundingboxListComponent } from './components/determination-graph/determination-endpoint/determination-endpoint-detail/boundingbox-list/boundingbox-list.component';

@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent,
    HomeComponent,
    DeterminationGraphComponent,
    QuestionNodeComponent,
    ButtonBarComponent,
    ButtonBarButtonComponent,
    DeterminationEndpointDetailComponent,
    DeterminationEndpointListComponent,
    BladeModalComponent,
    LoaderComponent,
    BoundingboxListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatInputModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatProgressSpinnerModule,
    MatCheckboxModule,
    MatMenuModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    LayoutModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
