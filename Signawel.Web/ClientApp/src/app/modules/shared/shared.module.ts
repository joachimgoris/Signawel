import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ButtonBarComponent } from "./components/button-bar/button-bar.component";
import { BladeModalComponent } from "./components/blade-modal/blade-modal.component";
import { ButtonBarButtonComponent } from "./components/button-bar-button/button-bar-button.component";
import { SignawelMaterialModule } from "../signawel-material/signawel-material.module";
import { ConfirmationDialogComponent } from './components/confirmation-dialog/confirmation-dialog.component';
import { MatDialogModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';

@NgModule({
  declarations: [
    ButtonBarComponent,
    ButtonBarButtonComponent,
    BladeModalComponent,
    ConfirmationDialogComponent
  ],
  imports: [CommonModule, SignawelMaterialModule, MatDialogModule],
  exports: [ButtonBarComponent, FlexLayoutModule, ButtonBarButtonComponent, BladeModalComponent]
})
export class SharedModule {}
