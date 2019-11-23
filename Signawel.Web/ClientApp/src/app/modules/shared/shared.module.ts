import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ButtonBarComponent } from "./components/button-bar/button-bar.component";
import { BladeModalComponent } from "./components/blade-modal/blade-modal.component";
import { ButtonBarButtonComponent } from "./components/button-bar-button/button-bar-button.component";
import { SignawelMaterialModule } from "../signawel-material/signawel-material.module";

@NgModule({
  declarations: [
    ButtonBarComponent,
    ButtonBarButtonComponent,
    BladeModalComponent
  ],
  imports: [CommonModule, SignawelMaterialModule],
  exports: [ButtonBarComponent, ButtonBarButtonComponent, BladeModalComponent]
})
export class SharedModule {}